using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Microsoft.AspNetCore.Mvc;

namespace TagSystem.Controllers.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private static AmazonDynamoDBClient _client;

        private AmazonDynamoDBClient GetClient()
        {
            if (_client == null)
            {
                AmazonDynamoDBConfig ddbConfig = new AmazonDynamoDBConfig();
                ddbConfig.ServiceURL = "http://localhost:8000";
                _client = new AmazonDynamoDBClient(ddbConfig);
            }
            return _client;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            var client = GetClient();
            var response = await client.ListTablesAsync();
            var isContain = response.TableNames.Contains("ABC");

            return Ok(isContain);
        }
    }
}