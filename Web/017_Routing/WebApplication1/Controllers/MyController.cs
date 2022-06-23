using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class MyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
