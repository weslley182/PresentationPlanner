using System.ComponentModel.DataAnnotations.Schema;

namespace PresentationPlanner.Areas.Contacts.Models;

public class Contact
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public DateTime Birth { get; set; }
    public string? PhotoUrl { get; set; }

    [NotMapped]
    public IFormFile? Picture { get; set; }
}
