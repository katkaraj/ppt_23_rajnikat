using Ppt23.Shared;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.EntityFrameworkCore;
using Ppt23.Api.Data;
using Mapster;

var builder = WebApplication.CreateBuilder(args);
var corsAllowedOrigin = builder.Configuration.GetSection("CorsAllowedOrigins").Get<string[]>();

string sqliteDbPath = builder.Configuration.GetValue<string>("sqliteDbPath");
//ArgumentNullException.ThrowIfNull(sqliteDbPath);
if(string.IsNullOrEmpty(sqliteDbPath)) { throw new ArgumentException(nameof(sqliteDbPath)); }

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PptDbContext>(opt => opt.UseSqlite($"FileName={sqliteDbPath}"));
builder.Services.AddScoped<SeedingData>();

builder.Services.AddCors(corsOptions => corsOptions.AddDefaultPolicy(policy =>
policy.WithOrigins(corsAllowedOrigin)
.WithMethods("GET", "DELETE", "POST", "PUT")
.AllowAnyHeader()));

var app = builder.Build();
app.UseCors();

app.Services.CreateScope().ServiceProvider
  .GetRequiredService<PptDbContext>()
  .Database.Migrate();

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
    return Results.Ok();
});

//Vrátí seznam vybavení
app.MapGet("/vybaveni_nemocnice", (PptDbContext db) =>
{
    Console.WriteLine($"Pocet vybaveni v db: {db.Vybavenis.Count()}");
    return db.Vybavenis.ToList();

});

//vrátí vybavení podle Id
app.MapGet("/vybaveni_nemocnice/{Id}", (Guid Id, PptDbContext db) =>
{
    Vybaveni? item = db.Vybavenis.ToList().SingleOrDefault(x => x.Id == Id);
    var en = item.Adapt<VybaveniRevizeVm>();
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
app.MapPut("/vybaveni_nemocnice/{Id}", (Guid Id, VybaveniVm vyb, PptDbContext db) =>
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

//revize vybavení - PUT*
app.MapPost("/revize/{Id}", (Guid Id, PptDbContext db) =>
{
    Vybaveni? vyb = db.Vybavenis.SingleOrDefault(x => x.Id == Id);
    var rev = new Revize();

    rev.Id = Guid.Empty;
    rev.Name = $"Revize {vyb.Name} #{vyb.Revizes.Count+1}";
    rev.VybaveniId = vyb.Id;
    rev.Vybaveni = vyb;
    rev.DateTime = DateTime.Today;
    db.Revizes.Add(rev);
    db.Vybavenis.SingleOrDefault(r => r.Id == rev.VybaveniId)?.Revizes.Add(rev);
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