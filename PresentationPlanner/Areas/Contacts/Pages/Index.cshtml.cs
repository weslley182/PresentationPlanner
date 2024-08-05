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

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        Contacts = await _serv.GetAllAsync(cancellationToken: cancellationToken);
    }
}
