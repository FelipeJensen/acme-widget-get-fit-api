using AcmeWidget.GetFit.Domain.Activities;

namespace AcmeWidget.GetFit.Application.ActivitySignUps.Dtos;

public class ActivityDateFiltered
{
    public long Id { get; }
    public DateTime StartDate { get; }
    public DateTime? EndDate { get; }
    public ActivityFrequency Frequency { get; }
    public List<ActivitySignUpFiltered> ActivitySignUps { get; }

    public ActivityDateFiltered(long id, DateTime startDate, DateTime? endDate, ActivityFrequency frequency, List<ActivitySignUpFiltered> activitySignUps)
    {
        Id = id;
        StartDate = startDate;
        EndDate = endDate;
        Frequency = frequency;
        ActivitySignUps = activitySignUps;
    }
}