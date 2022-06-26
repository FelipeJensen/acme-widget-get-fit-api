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

    public async Task<Result> Create(CreateActivity createActivity)
    {
        var validation = createActivity.Validate();
        if (validation.Any()) return new Result(validation);

        if (await _repository.Exists(createActivity.Name))
        {
            return new Result(Errors.General.EntityAlreadyExists(nameof(Errors.Activity)));
        }

        var activity = new Activity(createActivity.Name);

        if (createActivity.Dates.Any())
        {
            var results = createActivity.Dates.Select(p => ActivityDate.Build(p.StartDate, p.EndDate, p.Frequency, activity)).ToList();

            if (results.Any(p => !p.Success))
            {
                return new Result(results.Where(p => !p.Success).SelectMany(p => p.Errors).ToList());
            }

            foreach (var activityDate in results.Select(p => p.Value!))
            {
                activity.AddDate(activityDate);
            }
        }

        await _repository.Add(activity);

        await _repository.Persist();

        return new Result();
    }
}