using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;

        public DataController(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        [HttpGet]
        public IActionResult Get(string type)
        {
            IDataService dataService = type.ToLower() switch
            {
                "json" => this._serviceProvider.GetRequiredService<JsonService>(),
                "xml" => this._serviceProvider.GetRequiredService<XmlService>(),
            };

            return Ok($"oK: {dataService.GetData()}");
        }
    }
}