using AcmeWidget.GetFit.Domain.Activities;

namespace AcmeWidget.GetFit.Domain.ActivitySignups;

public class ActivitySignUp
{
    public long Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public int? YearsOfExperienceInActivity { get; private set; }
    public string? Comments { get; private set; }

    public long ActivityId { get; set; }
    public Activity Activity { get; set; }

    public long ActivityDateId { get; set; }
    public ActivityDate ActivityDate { get; private set; }

    #region EF

 #pragma warning disable CS8618
    // ReSharper disable once UnusedMember.Global
    protected ActivitySignUp()
    {

    }
 #pragma warning restore CS8618

    #endregion

    public ActivitySignUp(string firstName, string lastName, string email, Activity activity, ActivityDate activityDate, int? yearsOfExperienceInActivity, string? comments)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Activity = activity;
        ActivityDate = activityDate;
        YearsOfExperienceInActivity = yearsOfExperienceInActivity;
        Comments = comments;
    }
}