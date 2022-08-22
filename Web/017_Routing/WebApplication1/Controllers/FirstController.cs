using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class FirstController : Controller
    {
        public IActionResult Index()
        {
            return Ok("FirstController.Index");
        }
    }
}