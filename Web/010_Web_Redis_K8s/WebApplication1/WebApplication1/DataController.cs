using Microsoft.AspNetCore.Mvc;
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
            return "knight";
        }

        [HttpPost]
        public async Task<ActionResult> PostData([FromQuery][FromBody]string name)
        {
            return this.Ok(name);
        }
    }
}