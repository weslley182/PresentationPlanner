using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentationPlanner.Areas.Contacts.Models;
using PresentationPlanner.Areas.Contacts.Services.Interfaces;

namespace PresentationPlanner.Areas.Contacts.Pages;

public class CreateModel : PageModel
{
    [BindProperty]
    public Contact? Contact { get; set; }

    private readonly IContactService _contactService;

    public CreateModel(IContactService contactService)
    {
        _contactService = contactService;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var result = await _contactService.AddAsync(Contact, cancellationToken).ConfigureAwait(false);

        if (!result)
        {
            return RedirectToPage("/Error");
        }
        return RedirectToPage("Index");
    }
}
