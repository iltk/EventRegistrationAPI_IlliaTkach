using Microsoft.EntityFrameworkCore;
using EventRegistrationAPI_IlliaTkach.DTOs;
using EventRegistrationAPI_IlliaTkach.Models;

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
    
    public async Task<EventDto> CreateEventAsync(CreateEventDto dto)
    {
        var campusEvent = new Event
        {
            Title = dto.Title,
            Location = dto.Location,
            EventDate = dto.EventDate,
            Capacity = dto.Capacity
        };

        _db.Events.Add(campusEvent);

        await _db.SaveChangesAsync();

        return new EventDto
        {
            EventId = campusEvent.EventId,
            Title = campusEvent.Title,
            Location = campusEvent.Location,
            EventDate = campusEvent.EventDate,
            Capacity = campusEvent.Capacity,
            RegisteredCount = 0
        };
    }
    


   
}