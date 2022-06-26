namespace AcmeWidget.GetFit.Domain.Activities;

public class Activity
{
    private readonly List<ActivityDate> _activityDates = new();
    public long Id { get; private set; }
    public string Name { get; private set; }

    public IReadOnlyList<ActivityDate> Dates => _activityDates.AsReadOnly();

    public Activity(string name)
    {
        Name = name;
    }

    public void AddDate(ActivityDate activityDate)
    {
        _activityDates.Add(activityDate);
    }
}