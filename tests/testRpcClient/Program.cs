using engine.Wrapper;
using Microsoft.AspNetCore.Http.HttpResults;
using testRpcClient.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
var channel = RpcChannel.ForAddress(50000);
var weatherClient = RpcProxyFactory.Create<IWeatherService>(channel);
var result =  weatherClient.GetWeatherForcast();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapGet("/", () => $"Hello world");

app.MapGet("/weather", () => Results.Ok(result));



app.Run();


