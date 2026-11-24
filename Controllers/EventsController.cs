using Microsoft.AspNetCore.Mvc;
using EventRegistrationAPI_IlliaTkach.DTOs;
using EventRegistrationAPI_IlliaTkach.Services;

namespace EventRegistrationAPI_IlliaTkach.Controllers;

[ApiController]
[Route("api/events")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventsController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public async Task<ActionResult<List<EventDto>>> GetAllEvents()
    {
        var events = await _eventService.GetAllEventsAsync();

        return Ok(events);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EventDto>> GetEventById(int id)
    {
        var eventDto = await _eventService.GetEventByIdAsync(id);

        if (eventDto == null)
        {
            return NotFound();
        }

        return Ok(eventDto);
    }
    
    [HttpPost]
    public async Task<ActionResult<EventDto>> CreateEvent(CreateEventDto dto)
    {
        var createdEvent = await _eventService.CreateEventAsync(dto);

        return CreatedAtAction(
            nameof(GetEventById), 
            new { id = createdEvent.EventId }, createdEvent);
    }
}


