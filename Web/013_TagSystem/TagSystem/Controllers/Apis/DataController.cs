using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TagSystem.Services.DynamoDBs;

namespace TagSystem.Controllers.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IDynamoDBServcie _dynamoDBServcie;

        public DataController(IDynamoDBServcie dynamoDBServcie)
        {
            this._dynamoDBServcie = dynamoDBServcie;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            return Ok(string.Join(',', await this._dynamoDBServcie.TableList()));
        }

        [HttpGet("{tableName}")]
        public async Task<ActionResult> TableExist(string tableName)
        {
            return Ok(await this._dynamoDBServcie.TableExist(tableName));
        }
    }
}