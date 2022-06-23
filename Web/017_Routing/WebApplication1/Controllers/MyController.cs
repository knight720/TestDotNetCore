using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.ControllerBase
{
    [Route("api/[controller]")]
    public class MyController : Controller
    {
        [HttpGet("abc")]
        public IActionResult Index()
        {
            return Ok("MyController.Index");
        }
    }
}
