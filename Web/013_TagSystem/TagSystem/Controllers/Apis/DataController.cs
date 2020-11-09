using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TagSystem.Services;

namespace TagSystem.Controllers.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly DynamoDBService _dyanmoDBService;

        public DataController()
        {
            this._dyanmoDBService = new DynamoDBService();
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            var client = this._dyanmoDBService.GetClient();
            var response = await client.ListTablesAsync();
            var isContain = response.TableNames.Contains("ABC");

            return Ok(isContain);
        }
    }
}