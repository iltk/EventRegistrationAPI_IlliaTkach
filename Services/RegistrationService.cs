using Microsoft.EntityFrameworkCore;
using EventRegistrationAPI_IlliaTkach.DTOs;
using EventRegistrationAPI_IlliaTkach.DTOs;
using EventRegistrationAPI_IlliaTkach.Models;
using EventRegistrationAPI_IlliaTkach.Exceptions;

namespace EventRegistrationAPI_IlliaTkach.Services;

public class RegistrationService : IRegistrationService
{
    private readonly AppDbContext _db;

    public RegistrationService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<RegistrationDto>> GetRegistrationsAsync(int eventId)
    {
        if (!await _db.Events.AnyAsync(e => e.EventId == eventId))
        {
            throw new KeyNotFoundException(
                $"Event {eventId} not found.");
        }

        return await _db.Registrations
            .Where(r => r.EventId == eventId)
            .Select(r => new RegistrationDto
            {
                RegistrationId = r.RegistrationId,
                EventTitle = r.Event.Title,
                StudentName = r.StudentName,
                Email = r.Email,
                RegisteredOn = r.RegisteredOn
            })
            .ToListAsync();
    }

    public async Task<RegistrationDto> CreateRegistrationAsync(int eventId, CreateRegistrationDto dto)
    {
        var campusEvent = await _db.Events
            .FirstOrDefaultAsync(e => e.EventId == eventId);

        //event must exist
        if (campusEvent == null)
        {
            throw new KeyNotFoundException(
                $"Event {eventId} not found.");
        }

        // no past events
        if (campusEvent.EventDate <= DateTime.Now)
        {
            throw new InvalidOperationException(
                "Registration is closed for past events.");
        }

        // no duplicate registrations.
        var email = dto.Email.ToUpperInvariant();

        var alreadyRegistered = await _db.Registrations
            .AnyAsync(r => r.EventId == eventId && r.Email.ToUpper() == email);

        if (alreadyRegistered)
        {
            throw new ConflictException(
                "This email is already registered for this event.");
        }

        // check available capacity
        var registeredCount = await _db.Registrations
            .CountAsync(r => r.EventId == eventId);

        if (registeredCount >= campusEvent.Capacity)
        {
            throw new ConflictException("This event is full.");
        }

        var registration = new Registration
        {
            EventId = eventId,
            StudentName = dto.StudentName,
            Email = dto.Email,
            RegisteredOn = DateTime.Now
        };

        _db.Registrations.Add(registration);

        await _db.SaveChangesAsync();

        return new RegistrationDto
        {
            RegistrationId = registration.RegistrationId,
            EventTitle = campusEvent.Title,
            StudentName = registration.StudentName,
            Email = registration.Email,
            RegisteredOn = registration.RegisteredOn
        };
    }

    public async Task<bool> DeleteRegistrationAsync(int id)
    {
        var registration = await _db.Registrations.FindAsync(id);

        if (registration == null)
        {
            return false;
        }
        
        _db.Registrations.Remove(registration);

        await _db.SaveChangesAsync();

        return true;
    }
}