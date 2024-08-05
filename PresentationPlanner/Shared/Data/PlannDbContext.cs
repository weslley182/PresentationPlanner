using Microsoft.EntityFrameworkCore;
using PresentationPlanner.Areas.Contacts.Models;

namespace PresentationPlanner.Shared.Data;

public class PlannDbContext : DbContext
{
    public PlannDbContext(DbContextOptions<PlannDbContext> options) : base(options)
    {
    }

    public DbSet<Contact> Contacts { get; set; }
}
