using Microsoft.EntityFrameworkCore;
using PresentationPlanner.Areas.Contacts.Models;
using PresentationPlanner.Areas.Contacts.Repository.Interface;
using PresentationPlanner.Shared.Data;

namespace PresentationPlanner.Areas.Contacts.Repository;

public class ContactRepository : GenericRepository<Contact>, IContactRepository
{
    private readonly DbContext _dbContext;
    public ContactRepository(DbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public void UpdateContactWithoutPic(Contact contact)
    {
        Update(contact);
        _dbContext.Entry(contact).Property(x => x.PhotoUrl).IsModified = false;
    }
}
