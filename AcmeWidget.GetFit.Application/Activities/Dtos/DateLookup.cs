using AcmeWidget.GetFit.Domain.Activities;

namespace AcmeWidget.GetFit.Application.Activities.Dtos;

public class DateLookup
{
    public long Id { get; }
    public DateTime StartDate { get; }
    public DateTime? EndDate { get; }
    public ActivityFrequency Frequency { get; }

    public DateLookup(long id, DateTime startDate, DateTime? endDate, ActivityFrequency frequency)
    {
        Id = id;
        StartDate = startDate;
        EndDate = endDate;
        Frequency = frequency;
    }
}