namespace AcmeWidget.GetFit.Application.ActivitySignUps.Dtos;

public class ActivityFiltered
{
    public long Id { get; }
    public string Name { get; }
    public List<ActivityDateFiltered> ActivityDates { get; }

    public ActivityFiltered(long id, string name, List<ActivityDateFiltered> activityDates)
    {
        Id = id;
        Name = name;
        ActivityDates = activityDates;
    }
}