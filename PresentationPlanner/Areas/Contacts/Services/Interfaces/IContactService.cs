using PresentationPlanner.Areas.Contacts.Models;

namespace PresentationPlanner.Areas.Contacts.Services.Interfaces;

public interface IContactService
{
    Task<bool> AddAsync(Contact contact, CancellationToken cancellationToken);
}
