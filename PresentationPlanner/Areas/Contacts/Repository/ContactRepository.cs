using Microsoft.EntityFrameworkCore;
using PresentationPlanner.Areas.Contacts.Models;
using PresentationPlanner.Shared.Data;

namespace PresentationPlanner.Areas.Contacts.Repository;

public class ContactRepository : GenericRepository<Contact>
{
    public ContactRepository(DbContext dbContext) : base(dbContext)
    {
    }
}
