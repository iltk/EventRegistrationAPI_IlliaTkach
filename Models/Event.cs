using System.ComponentModel.DataAnnotations;

namespace EventRegistrationAPI_IlliaTkach.Models;

public class Event
{
    [Key]
    public int EventId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Location { get; set; } = string.Empty;

    public DateTime EventDate { get; set; }

    public int Capacity { get; set; }

    public ICollection<Registration> Registrations { get; set; }
        = new List<Registration>();
}