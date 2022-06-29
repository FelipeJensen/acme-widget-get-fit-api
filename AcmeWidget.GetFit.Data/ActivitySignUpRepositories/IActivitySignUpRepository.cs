using AcmeWidget.GetFit.Domain.Activities;
using AcmeWidget.GetFit.Domain.ActivitySignups;

namespace AcmeWidget.GetFit.Data.ActivitySignUpRepositories;

public interface IActivitySignUpRepository
{
    bool Exists(string email, long activityDateId);
    Task<ActivitySignUp?> Get(long id);
    Task Add(ActivitySignUp activitySignUp);
    Task Persist();
    Task<List<Activity>> GetActivitiesWithSignUp(string? name, long? activityId, long? activityDateId);
}