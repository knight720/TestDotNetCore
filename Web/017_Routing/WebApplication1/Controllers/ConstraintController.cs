using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    public class ConstraintController : Controller
    {
        [HttpGet("{value:int}")]
        public IActionResult Index_Int(int value)
        {
            return Ok($"Constraint Index_Int: {value}");
        }

        [HttpGet("{value:alpha}")]
        public IActionResult Index_Alpha(string value)
        {
            return Ok($"Constraint Index_Alpha: {value}");
        }
    }
}