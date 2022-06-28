using AcmeWidget.GetFit.Application.Activities.Dtos;
using AcmeWidget.GetFit.Data.ActivityRepositories;
using AcmeWidget.GetFit.Domain.Activities;
using AcmeWidget.GetFit.Domain.ResultHandling;

namespace AcmeWidget.GetFit.Application.Activities.UseCases.ActivitiesCreation;

public class ActivityCreation : IActivityCreation
{
    private readonly IActivityRepository _repository;

    public ActivityCreation(IActivityRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<long>> Create(CreateActivity createActivity)
    {
        var validation = createActivity.Validate();
        if (validation.Any()) return new Result<long>(validation);

        if (_repository.Exists(createActivity.Name))
        {
            return new Result<long>(Errors.General.EntityAlreadyExists(nameof(Errors.Activity)));
        }

        var activity = new Activity(createActivity.Name);

        if (createActivity.Dates.Any())
        {
            var results = createActivity.Dates.Select(p => ActivityDate.Build(p.StartDate, p.EndDate, p.Frequency, activity)).ToList();

            if (results.Any(p => !p.Success))
            {
                return new Result<long>(results.Where(p => !p.Success).SelectMany(p => p.Errors).ToList());
            }

            foreach (var activityDate in results.Select(p => p.Value!))
            {
                activity.AddDate(activityDate);
            }
        }

        await _repository.Add(activity);

        await _repository.Persist();

        return new Result<long>(activity.Id);
    }
}