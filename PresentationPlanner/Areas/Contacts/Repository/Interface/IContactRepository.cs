using PresentationPlanner.Areas.Contacts.Models;
using PresentationPlanner.Shared.Data;

namespace PresentationPlanner.Areas.Contacts.Repository.Interface;

public interface IContactRepository : IGenericRepository<Contact>
{
    void UpdateContactWithoutPic(Contact contact);
}
