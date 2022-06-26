using AcmeWidget.GetFit.Domain.Activities;
using AcmeWidget.GetFit.Domain.ActivitySignups;

namespace AcmeWidget.GetFit.Data.ActivitySignUpRepositories;

public interface IActivitySignUpRepository
{
    Task<Activity?> GetActivity(long id);
    Task<bool> Exists(string email, long activityId);
    Task<ActivityDate?> GetActivityDate(long activityDateId);
    Task Add(ActivitySignUp activitySignUp);
    Task Persist();
}