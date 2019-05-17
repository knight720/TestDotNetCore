using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        public IRequestCultureFeature RequestCulture { get; set; }

        public void OnGet()
        {
            RequestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
        }
    }
}