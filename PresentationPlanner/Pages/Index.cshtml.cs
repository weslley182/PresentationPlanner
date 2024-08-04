using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PresentationPlanner.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        _logger.LogWarning("Accessing get method.");
    }
}
