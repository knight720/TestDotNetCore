using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TagSystem.Models.Tags;
using TagSystem.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TagSystem.Controllers.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagsService _tagsService;

        public TagsController(ITagsService tagsService)
        {
            this._tagsService = tagsService;
        }

        // GET: api/<TagsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TagsController>/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            var result = this._tagsService.GetTag(id);
            return result;
        }

        // GET api/<TagsController>/5/1
        [HttpGet("{shopId}/{id}")]
        public ActionResult<IEnumerable<TagEntity>> Get(string shopId, string id)
        {
            var result = this._tagsService.GetTag(shopId, id);
            return this.Ok(result);
        }

        // POST api/<TagsController>
        [HttpPost]
        public void Post([FromBody] TagEntity tag)
        {
            this._tagsService.Create(tag);
        }

        // PUT api/<TagsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TagsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost("Query")]
        public ActionResult<IEnumerable<TagEntity>> Query([FromBody] TagQueryEntity queryEntity)
        {
            var result = this._tagsService.Query(queryEntity);
            return new ActionResult<IEnumerable<TagEntity>>(result);
        }
    }
}