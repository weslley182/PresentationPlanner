using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentationPlanner.Areas.Contacts.Models;
using PresentationPlanner.Areas.Contacts.Services.Interfaces;

namespace PresentationPlanner.Areas.Contacts.Pages;

public class CreateModel : PageModel
{
    [BindProperty]
    public Contact? Contact { get; set; } = new Contact();

    private readonly IContactService _contactService;

    public CreateModel(IContactService contactService)
    {
        _contactService = contactService;
    }

    public async Task OnGetAsync([FromRoute] Guid? id, CancellationToken cancellationToken)
    {
        if (id.HasValue)
        {
            Contact = await _contactService.GetById(id.Value, cancellationToken).ConfigureAwait(false);
        }
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

    public async Task<IActionResult> OnPostUpdateAsync(CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var result = await _contactService.UpdateAsync(Contact, cancellationToken).ConfigureAwait(false);
        if (!result)
        {
            return RedirectToPage("/Error");
        }

        return RedirectToPage("Index");
    }
}
