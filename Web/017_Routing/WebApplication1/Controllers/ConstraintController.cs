using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    public class ConstraintController : Controller
    {
        [HttpGet("{value:int}")]
        public IActionResult Index1(int value)
        {
            return Ok($"Constraint Index1 {value}");
        }

        [HttpGet("{value:alpha}")]
        public IActionResult Index2(string value)
        {
            return Ok($"Constraint Index2 {value}");
        }
    }
}