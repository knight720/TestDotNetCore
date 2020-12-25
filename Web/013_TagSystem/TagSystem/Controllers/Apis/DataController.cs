using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TagSystem.Models.DynamoDBs;
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
            return Ok(await this._dynamoDBServcie.TableList());
        }

        [HttpGet("{tableName}")]
        public async Task<ActionResult> TableExist(string tableName)
        {
            return Ok(await this._dynamoDBServcie.TableExist(tableName));
        }

        // POST api/<TagsController>
        [HttpPost("{tableName}")]
        public ActionResult Post(string tableName)
        {
            var result = this._dynamoDBServcie.CreateTable(tableName);

            if (result)
            {
                return Ok();
            }
            else
            {
                return this.StatusCode(500, "Create Fail");
            }
        }

        [HttpPost("Create")]
        public ActionResult PostCreate(DataModel dataModel)
        {
            var result = this._dynamoDBServcie.CreateTable(dataModel);

            if (result)
            {
                return Ok();
            }
            else
            {
                return this.StatusCode(500, "Create Fail");
            }
        }

        [HttpDelete("{tableName}")]
        public ActionResult Delete(string tableName)
        {
            var result = this._dynamoDBServcie.DeleteTable(tableName);

            if (result)
            {
                return Ok();
            }
            else
            {
                return this.StatusCode(500, "Delete Fail");
            }
        }
    }
}