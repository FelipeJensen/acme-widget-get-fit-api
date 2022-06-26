using AcmeWidget.GetFit.Domain.ResultHandling;

namespace AcmeWidget.GetFit.Domain.Activities;

public class ActivityDate
{
    public long Id { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public ActivityFrequency Frequency { get; private set; }

    public long ActivityId { get; set; }
    public Activity Activity { get; set; }

    #region EF

    #pragma warning disable CS8618
    // ReSharper disable once UnusedMember.Global
    protected ActivityDate()
     #pragma warning restore CS8618
    {

    }

    #endregion

    private ActivityDate(DateTime startDate, DateTime? endDate, ActivityFrequency frequency, Activity activity)
    {
        StartDate = startDate;
        EndDate = endDate;
        Activity = activity;

        if (endDate.HasValue)
        {
            Frequency = ActivityFrequency.Period;
        }
        else
        {
            Frequency = frequency;
        }
    }

    public static Result<ActivityDate> Build(DateTime startDate, DateTime? endDate, ActivityFrequency frequency, Activity activity)
    {
        if (startDate > endDate)
        {
            return new Result<ActivityDate>(Errors.ActivityDate.StartDateAfterEndDate(startDate, endDate.Value));
        }

        if (frequency == ActivityFrequency.Period && !endDate.HasValue)
        {
            return new Result<ActivityDate>(Errors.ActivityDate.PeriodFrequencyWithoutEndDate());
        }

        var activityDate = new ActivityDate(startDate, endDate, frequency, activity);

        return new Result<ActivityDate>(activityDate);
    }
}