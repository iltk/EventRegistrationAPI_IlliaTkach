using Microsoft.EntityFrameworkCore;
using EventRegistrationAPI_IlliaTkach.DTOs;

namespace EventRegistrationAPI_IlliaTkach.Services;

public class EventService : IEventService
{
    private readonly AppDbContext _db;

    public EventService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<EventDto>> GetAllEventsAsync()
    {
        return await _db.Events
            .OrderBy(e => e.EventDate)
            .Select(e => new EventDto
            {
                EventId = e.EventId,
                Title = e.Title,
                Location = e.Location,
                EventDate = e.EventDate,
                Capacity = e.Capacity,
                RegisteredCount = e.Registrations.Count()
            })
            .ToListAsync();
    }

    public async Task<EventDto?> GetEventByIdAsync(int id)
    {
        return await _db.Events
            .Where(e => e.EventId == id)
            .Select(e => new EventDto
            {
                EventId = e.EventId,
                Title = e.Title,
                Location = e.Location,
                EventDate = e.EventDate,
                Capacity = e.Capacity,
                RegisteredCount = e.Registrations.Count()
            })
            .FirstOrDefaultAsync();
    }
}