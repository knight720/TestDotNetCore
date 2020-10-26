using Microsoft.AspNetCore.Mvc;

namespace TagSystem.Controllers.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("OK");
        }
    }
}