using PresentationPlanner.Areas.Contacts.Models;

namespace PresentationPlanner.Areas.Contacts.Services.Interfaces;

public interface IContactService
{
    Task<bool> AddAsync(Contact contact, CancellationToken cancellationToken);

    Task<IEnumerable<Contact>> GetAllAsync(CancellationToken cancellationToken);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}
