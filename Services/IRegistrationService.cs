using EventRegistrationAPI_IlliaTkach.DTOs;

namespace EventRegistrationAPI_IlliaTkach.Services;

public interface IRegistrationService
{
    Task<List<RegistrationDto>> GetRegistrationsAsync(int eventId);

    Task<RegistrationDto> CreateRegistrationAsync(int eventId, CreateRegistrationDto dto);

    Task<bool> DeleteRegistrationAsync(int id);
}