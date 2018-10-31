using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISample _sample;

        public HomeController(ISample sample)
        {
            _sample = sample;
        }

        public string Index()
        {
            return $"[ISample]\r\n"
                 + $"Id: {_sample.Id}\r\n"
                 + $"HashCode: {_sample.GetHashCode()}\r\n"
                 + $"Tpye: {_sample.GetType()}";
        }
    }
}
