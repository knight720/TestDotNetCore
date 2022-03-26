using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAPI.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IConfiguration _configuration;

    public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
    {
        _logger = logger;
        this._configuration = configuration;
    }

    public void OnGet()
    {
        this._logger.LogDebug("Debug");
        this._logger.LogInformation("Information");
        this._logger.LogInformation($"Domain: {this._configuration["GOV_API:DomainName"]}");
    }
}