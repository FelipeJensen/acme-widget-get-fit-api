using AcmeWidget.GetFit.Domain.ResultHandling;

namespace AcmeWidget.GetFit.Application.Activities.Dtos;

public class CreateActivity
{
    public string Name { get; set; }
    public List<CreateActivityDate> Dates { get; set; } = new();

    public List<Error> Validate()
    {
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(Name))
        {
            errors.Add(Errors.Activity.ActivityNameEmpty());
        }

        errors.AddRange(Dates.SelectMany(p => p.IsValid()));

        return errors;
    }
}