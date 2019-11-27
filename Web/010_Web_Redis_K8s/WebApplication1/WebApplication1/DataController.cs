using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace WebApplication1
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public DataController(IServiceProvider serviceProvider, ILogger<DataController> logger)
        {
            this._configuration = serviceProvider.GetRequiredService<IConfiguration>();
            this._logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            var db = this.GetDatabase();
            var name = db.StringGet("mykey").ToString();

            return this.Ok(name);
        }

        [HttpPost]
        public async Task<ActionResult> PostData([FromQuery][FromBody]string name)
        {
            var db = this.GetDatabase();
            db.StringSet("mykey", name);

            return this.Ok($"Set Value [{name}] Success");
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private IDatabase GetDatabase()
        {
            var redisHost = this._configuration["Redis_Host"];
            this._logger.LogDebug(redisHost);

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(redisHost);
            IDatabase db = redis.GetDatabase();
            return db;
        }
    }
}