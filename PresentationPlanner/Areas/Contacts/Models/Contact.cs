using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PresentationPlanner.Areas.Contacts.Models;

public class Contact
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(30)]
    [MinLength(3)]
    public string? Name { get; set; }

    [Required]
    [RegularExpression(@"^[1-9][0-9]{9}$", ErrorMessage = "Invalid phone number.")]
    public string? Phone { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Invalid Email.")]
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime Birth { get; set; }
    public string? PhotoUrl { get; set; }

    [NotMapped]
    public IFormFile? Picture { get; set; }
}
