using Ppt23.Shared;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(corsOptions => corsOptions.AddDefaultPolicy(policy =>
policy.WithOrigins("https://localhost:1111")
.WithMethods("GET", "DELETE", "POST", "PUT")
.AllowAnyHeader()));

var app = builder.Build();
app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


List<VybaveniVm> seznamVybaveni = VybaveniVm.GetTestList(10);

//Vrátí seznam vybavení
app.MapGet("/vybaveni_nemocnice", () =>
{
    return seznamVybaveni;

});//.WithOpenApi();


//Přidá nové vybavení do listu
app.MapPost("/vybaveni_nemocnice", (VybaveniVm prichoziModel) =>
{
    prichoziModel.Id = Guid.NewGuid();
    seznamVybaveni.Insert(0, prichoziModel);
});

//Smaže vybavení podle Id
app.MapDelete("/vybaveni_nemocnice/{Id}", (Guid Id) =>
{
    var item = seznamVybaveni.SingleOrDefault(x => x.Id == Id);
    if (item == null)
        return Results.NotFound("Tato položka nebyla nalezena");
    seznamVybaveni.Remove(item);
    return Results.Ok();
});

//Upraví vybavení v listu (podle Id)
app.MapPut("/vybaveni_nemocnice/{Id}", (Guid Id, VybaveniVm vyb) =>
{
    var item = seznamVybaveni.SingleOrDefault(x => x.Id == Id);
    if(item == null)
    {
        return Results.NotFound("Tato položka nebyla nalezena");
    }
    else
    {
        vyb.Id = Id;
        seznamVybaveni.Remove(vyb);
        seznamVybaveni.Insert(0, vyb);
        return Results.Ok(vyb);
    }
});

//vrátí vybavení podle Id
app.MapGet("/vybaveni_nemocnice/{Id}", (Guid Id) =>
{
    var item = seznamVybaveni.SingleOrDefault(x => x.Id == Id);
    if (item is null)
        return Results.NotFound("Tato položka nebyla nalezena");
    return Results.Ok(item);

});

app.MapPut("/vybaveni_nemocnice/revize/{Id}", (Guid Id) =>
{
    var item = seznamVybaveni.SingleOrDefault(x => x.Id == Id);
    if(item == null)
    {
        return Results.NotFound("Tato položka nebyla nalezena");
    }
    else
    {
        seznamVybaveni[seznamVybaveni.IndexOf(item)].LastRevisionDateTime = DateTime.Today;
        return Results.Ok();
    }
});


app.Run();


//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast =  Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast")
//.WithOpenApi();
//record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}

