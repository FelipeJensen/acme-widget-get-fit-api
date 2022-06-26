using AcmeWidget.GetFit.Domain.Activities;

namespace AcmeWidget.GetFit.Domain.ActivitySignups;

public class ActivitySignUp
{
    public long Id { get; private set; }
    public string Name { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public Activity Activity { get; private set; }
    public ActivityDate ActivityDate { get; private set; }
    public int? YearsOfExperienceInActivity { get; private set; }
    public string? Comments { get; private set; }

    public ActivitySignUp(string name, string lastName, string email, Activity activity, ActivityDate activityDate, int? yearsOfExperienceInActivity, string? comments)
    {
        Name = name;
        LastName = lastName;
        Email = email;
        Activity = activity;
        ActivityDate = activityDate;
        YearsOfExperienceInActivity = yearsOfExperienceInActivity;
        Comments = comments;
    }
}