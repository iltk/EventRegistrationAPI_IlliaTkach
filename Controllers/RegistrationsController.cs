using Microsoft.AspNetCore.Mvc;
using EventRegistrationAPI_IlliaTkach.DTOs;
using EventRegistrationAPI_IlliaTkach.Services;
using EventRegistrationAPI_IlliaTkach.Exceptions;

namespace EventRegistrationAPI_IlliaTkach.Controllers;

[ApiController]
[Route("api")]
public class RegistrationsController : ControllerBase
{
    private readonly IRegistrationService _registrationService;

    public RegistrationsController(IRegistrationService registrationService)
    {
        _registrationService = registrationService;
    }

    [HttpGet("events/{id}/registrations")]
    public async Task<ActionResult<List<RegistrationDto>>> GetRegistrations(int id)
    {
        try
        {
            var registrations = await _registrationService.GetRegistrationsAsync(id);
            return Ok(registrations);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost("events/{id}/registrations")]
    public async Task<ActionResult<RegistrationDto>> CreateRegistration(int id, CreateRegistrationDto dto)
    {
        try
        {
            var registration = await _registrationService.CreateRegistrationAsync(id, dto);
            return StatusCode(201, registration);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ConflictException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpDelete("registrations/{id}")]
    public async Task<IActionResult> DeleteRegistration(int id)
    {
        var deleted = await _registrationService.DeleteRegistrationAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}