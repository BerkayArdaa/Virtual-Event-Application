using Microsoft.AspNetCore.Mvc;

namespace WEBAPI.Controllers
{
    // API controller for demonstrating a sample weather forecast endpoint
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        // Predefined array of weather summaries
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild",
            "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        // Constructor for dependency injection of logger
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        // GET: /WeatherForecast
        // Returns a list of 5 random weather forecasts
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                // Sets the date to today + index days
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),

                // Generates a random temperature in Celsius
                TemperatureC = Random.Shared.Next(-20, 55),

                // Selects a random summary
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
