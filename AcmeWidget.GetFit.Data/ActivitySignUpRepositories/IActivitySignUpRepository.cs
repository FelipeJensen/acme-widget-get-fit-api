using AcmeWidget.GetFit.Domain.ActivitySignups;

namespace AcmeWidget.GetFit.Data.ActivitySignUpRepositories;

public interface IActivitySignUpRepository
{
    bool Exists(string email, long activityId);
    Task<ActivitySignUp?> Get(long id);
    Task Add(ActivitySignUp activitySignUp);
    Task Persist();
}