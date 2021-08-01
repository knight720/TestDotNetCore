using System;
using System.Net;
using System.Runtime.Loader;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private bool _stopping = false;
        private bool _started = false;
        public static bool Unloading = false;

        static WeatherForecastController()
        {
            AssemblyLoadContext.Default.Unloading += Default_Unloading;
        }

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHostApplicationLifetime hostApplicationLifetime)
        {
            _logger = logger;
            this._hostApplicationLifetime = hostApplicationLifetime;
            this._hostApplicationLifetime.ApplicationStarted.Register(() => this._started = true);
            this._hostApplicationLifetime.ApplicationStopping.Register(() =>
            {
                this._stopping = true;
                Thread.Sleep(6000);
            });

            this._logger.LogInformation($"Started: {this._started}");
        }

        private static void Default_Unloading(AssemblyLoadContext obj)
        {
            WeatherForecastController.Unloading = true;
            Thread.Sleep(3000);
        }

        [HttpGet]
        public IActionResult Get(CancellationToken cancellationToken)
        {
            this._logger.LogInformation($"{DateTime.Now.ToString("mm:ss")}");
            this._logger.LogInformation($"Stopping: {this._stopping}");
            this._logger.LogInformation($"token: {cancellationToken.IsCancellationRequested}");
            this._logger.LogInformation($"Unloading: {WeatherForecastController.Unloading}");

            if (_stopping == true)
            {
                this._logger.LogInformation("503");
                return this.StatusCode((int)HttpStatusCode.ServiceUnavailable);
            }

            if (cancellationToken.IsCancellationRequested == true)
            {
                this._logger.LogInformation("404");
                return this.StatusCode((int)HttpStatusCode.NotFound);
            }

            if (WeatherForecastController.Unloading == true)
            {
                this._logger.LogInformation("500");
                return this.StatusCode((int)HttpStatusCode.InternalServerError);
            }

            this._logger.LogInformation("200");
            return this.Ok(DateTime.Now.Ticks);
        }
    }
}