using System.ComponentModel.DataAnnotations;
using AcmeWidget.GetFit.Domain.ResultHandling;

namespace AcmeWidget.GetFit.Application.ActivitySignUps.Dtos;

public class CreateActivitySignUp
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    [EmailAddress]
    public string Email { get; set; } = null!;

    public long ActivityId { get; set; }
    public long ActivityDateId { get; set; }
    public int? YearsOfExperienceInActivity { get; set; }
    public string? Comments { get; set; } = null!;

    public List<Error> Validate()
    {
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(FirstName))
        {
            errors.Add(Errors.General.Required("First name"));
        }

        if (string.IsNullOrWhiteSpace(LastName))
        {
            errors.Add(Errors.General.Required("Last name"));
        }

        if (string.IsNullOrWhiteSpace(Email))
        {
            errors.Add(Errors.General.Required("Email"));
        }

        return errors;
    }
}