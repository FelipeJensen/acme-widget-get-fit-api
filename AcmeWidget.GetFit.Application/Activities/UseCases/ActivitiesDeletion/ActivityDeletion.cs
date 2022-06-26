using AcmeWidget.GetFit.Data.ActivityRepositories;
using AcmeWidget.GetFit.Domain.Activities;
using AcmeWidget.GetFit.Domain.ResultHandling;

namespace AcmeWidget.GetFit.Application.Activities.UseCases.ActivitiesDeletion;

public class ActivityDeletion : IActivityDeletion
{
    private readonly IActivityRepository _repository;

    public ActivityDeletion(IActivityRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Delete(long id)
    {
        var activity = await _repository.Get(id);

        if (activity == null) return new Result(Errors.General.NotFound(nameof(Activity)));

        _repository.Delete(activity);

        await _repository.Persist();

        return new Result();
    }
}