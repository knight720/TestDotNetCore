using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace WebApplication1
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
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
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            IDatabase db = redis.GetDatabase();
            return db;
        }
    }
}