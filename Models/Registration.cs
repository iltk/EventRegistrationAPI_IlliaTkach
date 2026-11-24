using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventRegistrationAPI_IlliaTkach.Models;

public class Registration
{
    [Key]
    public int RegistrationId { get; set; }

    public int EventId { get; set; }

    [Required]
    public string StudentName { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    public DateTime RegisteredOn { get; set; }

    [ForeignKey(nameof(EventId))]
    public Event Event { get; set; } = null!;
}