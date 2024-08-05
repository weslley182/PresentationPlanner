using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentationPlanner.Areas.Contacts.Models;

namespace PresentationPlanner.Areas.Contacts.Pages;

public class CreateModel : PageModel
{
    [BindProperty]
    public Contact? Contact { get; set; }
    public IWebHostEnvironment Env { get; }

    public CreateModel(IWebHostEnvironment env)
    {
        Env = env;
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

        Contact.Id = Guid.NewGuid();
        Contact.PhotoUrl = Path.Combine("Images", "Contacts", $"{Contact.Id}-{Contact.Picture.FileName}");
        var fullpath = Path.Combine(Env.WebRootPath, Contact.PhotoUrl);
        using (var fileStream = new FileStream(fullpath, FileMode.Create))
        {
            await Contact.Picture.CopyToAsync(fileStream, cancellationToken);
        }

        return RedirectToPage();
    }
}
