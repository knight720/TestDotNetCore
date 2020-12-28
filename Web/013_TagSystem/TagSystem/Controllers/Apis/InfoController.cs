using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace TagSystem.Controllers.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _enviroment;

        public InfoController(IConfiguration configuration, IWebHostEnvironment enviroment)
        {
            this._configuration = configuration;
            this._enviroment = enviroment;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            var infoDictionary = new Dictionary<string, string>
            {
                {"Environment", this._enviroment.EnvironmentName },
                {"Config", this._configuration["Debug"] },
            };

            return Ok(infoDictionary);
        }
    }
}