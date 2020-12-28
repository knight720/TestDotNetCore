using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TagSystem.Models.DynamoDBs;
using TagSystem.Services.DynamoDBs;

namespace TagSystem.Controllers.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly IDynamoDBServcie _dynamoDBServcie;

        public TablesController(IDynamoDBServcie dynamoDBServcie)
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
            try
            {
                this._dynamoDBServcie.CreateTable(tableName);
                return Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpPost("Create")]
        public ActionResult PostCreate(DataModel dataModel)
        {
            try
            {
                this._dynamoDBServcie.CreateTable(dataModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpDelete("{tableName}")]
        public ActionResult Delete(string tableName)
        {
            try
            {
                this._dynamoDBServcie.DeleteTable(tableName);

                return Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, $"{ex.Message}");
            }
        }
    }
}