using EventRegistrationAPI_IlliaTkach.DTOs;
using EventRegistrationAPI_IlliaTkach.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventRegistrationAPI_IlliaTkach.Services;

public interface IEventService
{
    
    public Task<List<EventDto>> GetAllEventsAsync();
    
    Task<EventDto?> GetEventByIdAsync(int id);
    
}