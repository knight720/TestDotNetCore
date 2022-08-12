using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    public class SecondController : Controller
    {
        public IActionResult Index()
        {
            return Ok("SecondController.Index");
        }

        public IActionResult Index_Two()
        {
            return Ok("SecondController.Index_Two");
        }
    }
}