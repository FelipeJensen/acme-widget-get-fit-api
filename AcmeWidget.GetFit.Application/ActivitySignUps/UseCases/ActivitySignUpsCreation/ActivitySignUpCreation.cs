using AcmeWidget.GetFit.Application.ActivitySignUps.Dtos;
using AcmeWidget.GetFit.Data.ActivityRepositories;
using AcmeWidget.GetFit.Data.ActivitySignUpRepositories;
using AcmeWidget.GetFit.Domain.ActivitySignups;
using AcmeWidget.GetFit.Domain.ResultHandling;

namespace AcmeWidget.GetFit.Application.ActivitySignUps.UseCases.ActivitySignUpsCreation;

public class ActivitySignUpCreation : IActivitySignUpCreation
{
    private readonly IActivitySignUpRepository _repository;
    private readonly IActivityRepository _activityRepository;

    public ActivitySignUpCreation(IActivitySignUpRepository repository, IActivityRepository activityRepository)
    {
        _repository = repository;
        _activityRepository = activityRepository;
    }

    public async Task<Result<long>> Create(CreateActivitySignUp createSignUp)
    {
        var validation = createSignUp.Validate();
        if (validation.Any()) return new Result<long>(validation);

        var activity = await _activityRepository.Get(createSignUp.ActivityId);

        if (activity == null) return new Result<long>(Errors.General.NotFound(nameof(Errors.Activity)));

        var activityDate = await _activityRepository.GetDate(createSignUp.ActivityDateId);

        if (activityDate == null) return new Result<long>(Errors.General.NotFound(nameof(Errors.ActivityDate)));

        if (AlreadySignedUp(createSignUp)) return new Result<long>(Errors.ActivitySignUp.AlreadySignedUp(createSignUp.Email, activity.Name));

        var activitySignUp = new ActivitySignUp(
            createSignUp.FirstName,
            createSignUp.LastName,
            createSignUp.Email,
            activity,
            activityDate,
            createSignUp.YearsOfExperienceInActivity,
            createSignUp.Comments
        );

        await _repository.Add(activitySignUp);

        await _repository.Persist();

        // TODO: send confirmation email with unsubscribe code

        return new Result<long>(activitySignUp.Id);
    }

    private bool AlreadySignedUp(CreateActivitySignUp createSignUp)
    {
        return _repository.Exists(createSignUp.Email, createSignUp.ActivityDateId);
    }
}