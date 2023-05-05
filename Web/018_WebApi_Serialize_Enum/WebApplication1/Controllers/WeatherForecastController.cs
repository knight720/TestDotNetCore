using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Converters;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [Route("[action]")]
        [HttpGet]
        public BClass GetSerialize()
        {
            return new BClass();
        }
    }

    public enum AType
    {
        A1,
        A2,
    }

    // by enum
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BType
    {
        B1,
        B2,
    }

    public enum CType
    {
        C1,
        C2,
    }

    public class AClass
    {
        // by property
        [JsonConverter(typeof(StringEnumConverter))]
        public AType AType { get; set; }

        public BType BType { get; set; }
        public CType CType { get; set; }
    }

    public class BClass : AClass
    {
    }

    public class StringEnumConverter : JsonStringEnumConverter
    {
    }
}