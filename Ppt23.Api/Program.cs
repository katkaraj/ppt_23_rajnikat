using Mapster;
using Ppt23.Shared;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Ppt23.Api.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<SeedingData>();

var corsAllowedOrigin = builder.Configuration.GetSection("CorsAllowedOrigins").Get<string[]>();
ArgumentNullException.ThrowIfNull(corsAllowedOrigin);

builder.Services.AddCors(corsOptions => corsOptions.AddDefaultPolicy(policy =>
policy.WithOrigins(corsAllowedOrigin)
.WithMethods("GET", "DELETE", "POST", "PUT")
.AllowAnyHeader()));


string? sqliteDbPath = builder.Configuration[nameof(sqliteDbPath)];
ArgumentNullException.ThrowIfNull(sqliteDbPath);
//if(string.IsNullOrEmpty(sqliteDbPath)) { throw new ArgumentException(nameof(sqliteDbPath)); }
builder.Services.AddDbContext<PptDbContext>(opt => opt.UseSqlite($"FileName={sqliteDbPath}"));

var app = builder.Build();

//app.Services.CreateScope().ServiceProvider
  //.GetRequiredService<PptDbContext>()
  //.Database.Migrate();

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


//List<VybaveniVm> seznamVybaveni = VybaveniVm.GetTestList(10);
//List<RevizeViewModel> seznamRevizi = RevizeViewModel.GetRevizeList(5);

//vyhledávání v revizích
app.MapGet("/revize/{nazdar}", (string nazdar, PptDbContext db) =>
{
    var list = db.Revizes.ToList();
    var filtrRevize = list.Where(x => x.Name.Contains(nazdar)).Adapt<List<RevizeViewModel>>();
    db.SaveChanges();
    return Results.Ok();
});

//Vrátí seznam vybavení
app.MapGet("/vybaveni_nemocnice", (PptDbContext db) =>
{
    
    List<VybaveniVm> vyblist = new List<VybaveniVm>();
    var vyb = db.Vybavenis.ToList();
    var rev = db.Revizes.ToList();

    foreach(var o in vyb)
    {
        VybaveniVm? en = db.Vybavenis.SingleOrDefault(x => x.Id == o.Id).Adapt<VybaveniVm>();
        var vybRev = rev.Where(r => r.VybaveniId == en.Id).ToList().OrderByDescending(x => x.DateTime);
        if (vybRev.Any())
        {
            en.LastRevisionDateTime = vybRev.First().DateTime;
        }
        else
        {
            en.LastRevisionDateTime = en.BoughtDateTime;
        }
        vyblist.Add(en);
    }
    return vyblist;
});

app.MapGet("/revize/{Id}", (Guid Id, PptDbContext db) =>
{
    var rev = db.Revizes.ToList().Where(x => x.Id == Id);

    return rev;
});

//vrátí detail vybavení podle Id
app.MapGet("/vybaveni_nemocnice/{Id}", (Guid vId, PptDbContext db) =>
{
    Vybaveni? item = db.Vybavenis
    .Include(x => x.Revizes)
    .Include(x => x.Ukons)
    .SingleOrDefault(x => x.Id == vId);
    var en = item?.Adapt<VybaveniRevizeVm>();
    db.SaveChanges();
    return en;

});

//Přidá=POST nové vybavení do listu 
app.MapPost("/vybaveni_nemocnice", (VybaveniVm prichoziModel, PptDbContext db) =>
{
    prichoziModel.Id = Guid.Empty;
    var en = prichoziModel.Adapt<Vybaveni>();
    db.Vybavenis.Add(en);
    db.SaveChanges();
    return prichoziModel.Id;
});

//Smaže vybavení podle Id
app.MapDelete("/vybaveni_nemocnice/{Id}", (Guid Id, PptDbContext db) =>
{
    var item = db.Vybavenis.SingleOrDefault(x => x.Id == Id);
    if (item == null)
        return Results.NotFound("Tato položka nebyla nalezena");
    db.Vybavenis.Remove(item);
    db.SaveChanges();
    return Results.Ok();
});

//Upraví=PUT vybavení v listu (podle Id)*
app.MapPut("/vybaveni_nemocnice/{Id}", (Guid Id, Vybaveni vyb, PptDbContext db) =>
{
    var item = db.Vybavenis.SingleOrDefault(x => x.Id == Id);
    if(item == null)
    {
        return Results.NotFound("Tato položka nebyla nalezena");
    }
    else
    {
        vyb.Id = Id;
        db.Vybavenis.Entry(item).CurrentValues.SetValues(vyb);
        db.SaveChanges();
        return Results.Ok(vyb);
    }
});

//revize vybavení - POST*
app.MapPost("/revize/{Id}", (Guid Id, PptDbContext db) =>
{
    Vybaveni? vyb = db.Vybavenis.SingleOrDefault(x => x.Id == Id);
    var rev = new Revize();

    rev.Id = Guid.Empty;
    rev.Name = $"Revize {vyb.Name} #{vyb.Revizes.Count+1}";
    rev.VybaveniId = vyb.Id;
    rev.Vybaveni = vyb;
    rev.DateTime = DateTime.Today;
   
    db.Vybavenis.SingleOrDefault(r => r.Id == rev.VybaveniId)?.Revizes.Add(rev);
    db.SaveChanges();
});

using var appContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<PptDbContext>();

try
{
    appContext.Database.Migrate();
}
catch (Exception ex)
{
    Console.WriteLine($"Exception during db migration {ex.Message}");
}

await app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedingData>().SeedData();

app.Run();