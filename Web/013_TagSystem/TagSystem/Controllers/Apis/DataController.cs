using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TagSystem.Services;

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
            var client = this._dynamoDBServcie.GetClient();
            this._dynamoDBServcie.CreateTable(DateTime.Now.ToString("HHmmss"));
            var response = await client.ListTablesAsync();
            var isContain = response.TableNames.Contains("ABC");

            return Ok(string.Join(',', response.TableNames));
        }

        [HttpGet("{tableName}")]
        public async Task<ActionResult> TableExist(string tableName)
        {
            return Ok(await this._dynamoDBServcie.TableExist(tableName));
        }
    }
}