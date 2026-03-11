using engine.Wrapper;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
var client = RpcChannel.ForAddress(50000);
var result = await client.CallAsync("WeatherService", "GetAllWeatherForcast");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapGet("/", () => $"Response Id :{result.Id}");

app.MapGet("/weather", () => Results.Ok(result.Data));



app.Run();


