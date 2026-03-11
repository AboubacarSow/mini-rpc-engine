
using engine.Extensions;
using testRpcServer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRpcEngine();
builder.Services.AddServices(config =>
{
    config.AddService(nameof(WeatherService), new WeatherService());
});
builder.Services.AddScoped<WeatherService>();

var app = builder.Build();


app.Run();
