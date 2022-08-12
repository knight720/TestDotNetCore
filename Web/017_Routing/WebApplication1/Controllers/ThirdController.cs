using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    public class ThirdController : Controller
    {
        [HttpGet("[action]")]
        public IActionResult Index()
        {
            return Ok("ThirdController.Index");
        }

        [HttpGet("Two")]
        public IActionResult Index_Two()
        {
            return Ok("ThirdController.Index_Two");
        }

        [HttpGet("Three/{id}")]
        public IActionResult Index_Three(int id)
        {
            return Ok($"ThirdController.Index_Three. id: {id}");
        }
    }
}