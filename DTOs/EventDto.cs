namespace EventRegistrationAPI_IlliaTkach.DTOs;

public class EventDto
{
    public int EventId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public DateTime EventDate { get; set; }

    public int Capacity { get; set; }

    public int RegisteredCount { get; set; }

    public int SpotsLeft => Capacity - RegisteredCount;
}