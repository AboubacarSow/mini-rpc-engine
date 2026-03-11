namespace testRpcServer.Services;

public class WeatherService
{
    private List<string> _summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly List<WeatherForcast> _weatherForcasts=Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            _summaries[Random.Shared.Next(_summaries.Length)]
        ))
        .ToArray();
    
    public List<WeatherForcast> GetAllWeatherForcast()
    {
        return _weatherForcasts;
    }
}