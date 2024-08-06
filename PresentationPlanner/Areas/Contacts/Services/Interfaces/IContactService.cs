using PresentationPlanner.Areas.Contacts.Models;

namespace PresentationPlanner.Areas.Contacts.Services.Interfaces;

public interface IContactService
{
    Task<bool> AddAsync(Contact contact, CancellationToken cancellationToken);
    Task<IEnumerable<Contact>> GetAllAsync(CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<Contact> GetById(Guid id, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Contact contact, CancellationToken cancellationToken);

    //Task<bool> UpdateFavorite(Guid id, CancellationToken cancellationToken);
    //Task<int> CountContacts(CancellationToken cancellationToken);
}
