using testRpcServer.Models;

namespace testRpcServer.Services;

public class WeatherService
{
 

    private readonly List<WeatherForcast> _weatherForcasts= [.. Enumerable.Range(1, 5).Select(index =>
        new WeatherForcast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            WeatherForcast.Summaries[Random.Shared.Next(WeatherForcast.Summaries.Count)]
        ))];
    
    public List<WeatherForcast> GetAllWeatherForcast()
    {
        return _weatherForcasts;
    }
}