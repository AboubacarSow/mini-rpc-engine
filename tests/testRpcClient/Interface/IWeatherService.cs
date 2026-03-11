namespace testRpcClient.Interface
{
    public interface IWeatherService
    {
        List<WeatherForcast> GetAllWeatherForcast();
    }

    public record WeatherForcast(DateOnly Date, int TemperatureC, string? Summary)
    {
       
    }
}
