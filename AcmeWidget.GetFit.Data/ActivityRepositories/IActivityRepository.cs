using AcmeWidget.GetFit.Domain.Activities;

namespace AcmeWidget.GetFit.Data.ActivityRepositories;

public interface IActivityRepository
{
    Task<bool> Exists(string name);
    Task Persist();
    Task Add(Activity activity);
}