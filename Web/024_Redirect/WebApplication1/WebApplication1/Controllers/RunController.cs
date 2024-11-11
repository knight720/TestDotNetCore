using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class RunController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
