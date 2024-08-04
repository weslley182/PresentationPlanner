using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentationPlanner.Areas.Contacts.Models;

namespace PresentationPlanner.Areas.Contacts.Pages;

public class CreateModel : PageModel
{
    public Contact? Contact { get; set; }
    public void OnGet()
    {
    }
}
