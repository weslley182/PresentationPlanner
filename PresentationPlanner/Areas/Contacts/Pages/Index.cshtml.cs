using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentationPlanner.Areas.Contacts.Models;
using PresentationPlanner.Areas.Contacts.Services.Interfaces;

namespace PresentationPlanner.Areas.Contacts.Pages;

public class IndexModel : PageModel
{
    private readonly IContactService _serv;
    private const int QTD_BY_PAGE = 6;
    public IEnumerable<Contact> Contacts { get; set; } = new List<Contact>();
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }

    public IndexModel(IContactService serv)
    {
        _serv = serv;
    }

    public async Task OnGetAsync([FromQuery] int currentPage = 1, CancellationToken cancellationToken = default)
    {
        CurrentPage = currentPage;
        var qtdContacts = await _serv.CountContacts(cancellationToken).ConfigureAwait(false);
        TotalPages = (int)Math.Ceiling((double)qtdContacts / QTD_BY_PAGE);

        Contacts = await _serv.GetAllAsync(currentPage, QTD_BY_PAGE, cancellationToken).ConfigureAwait(false);
    }

    public async Task<IActionResult> OnPostFavoriteAsync([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        var result = await _serv.ChangeFavorite(id, cancellationToken).ConfigureAwait(false);
        if (!result)
        {
            return RedirectToPage("/Erro");
        }
        return RedirectToPage("Index");
    }
}
