using System.ComponentModel.DataAnnotations;

namespace EventRegistrationAPI_IlliaTkach.DTOs;

public class CreateRegistrationDto
{
    [Required(ErrorMessage = "Student name is required.")]
    public string StudentName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; } = string.Empty;
}