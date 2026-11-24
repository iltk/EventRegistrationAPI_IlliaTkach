using System.ComponentModel.DataAnnotations;

namespace EventRegistrationAPI_IlliaTkach.Validation;

public class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        return value is DateTime date && date > DateTime.Now;
    }
}