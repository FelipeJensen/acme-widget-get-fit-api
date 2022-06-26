using AcmeWidget.GetFit.Domain.ActivitySignups;

namespace AcmeWidget.GetFit.Domain.Activities;

public class Activity
{
    public long Id { get; private set; }
    public string Name { get; private set; }

    public List<ActivitySignUp> ActivitySignUps { get; set; } = new();
    public List<ActivityDate> ActivityDates { get; set; } = new();

    public Activity(string name)
    {
        Name = name;
    }

    public void AddDate(ActivityDate activityDate)
    {
        ActivityDates.Add(activityDate);
    }
}