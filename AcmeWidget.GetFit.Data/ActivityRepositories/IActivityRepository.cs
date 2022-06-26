using AcmeWidget.GetFit.Domain.Activities;

namespace AcmeWidget.GetFit.Data.ActivityRepositories;

public interface IActivityRepository
{
    bool Exists(string name);
    Task<Activity?> Get(long id);
    Task Add(Activity activity);
    void Delete(Activity activity);
    Task Persist();
    IEnumerable<Lookup<long>> Lookup();
    IEnumerable<ActivityDate?> ActivityDates(long id);
}