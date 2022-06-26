using AcmeWidget.GetFit.Domain.Activities;
using AcmeWidget.GetFit.Domain.ResultHandling;

namespace AcmeWidget.GetFit.Application.Activities.Dtos;

public class CreateActivityDate
{
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ActivityFrequency Frequency { get; set; }

    public List<Error> IsValid()
    {
        var errors = new List<Error>();

        // TODO: validate Enum

        return errors;
    }
}