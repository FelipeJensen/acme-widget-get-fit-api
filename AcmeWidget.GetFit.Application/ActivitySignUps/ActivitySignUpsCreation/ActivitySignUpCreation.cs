﻿using AcmeWidget.GetFit.Application.ActivitySignUps.Dtos;
using AcmeWidget.GetFit.Data.ActivitySignUpRepositories;
using AcmeWidget.GetFit.Domain.ActivitySignups;
using AcmeWidget.GetFit.Domain.ResultHandling;

namespace AcmeWidget.GetFit.Application.ActivitySignUps.ActivitySignUpsCreation;

public class ActivitySignUpCreation : IActivitySignUpCreation
{
    private readonly IActivitySignUpRepository _repository;

    public ActivitySignUpCreation(IActivitySignUpRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Create(CreateActivitySignUp createSignUp)
    {
        var validation = createSignUp.Validate();
        if (validation.Any()) return new Result(validation);

        var activity = await _repository.GetActivity(createSignUp.ActivityId);

        if (activity == null) return new Result(Errors.General.NotFound(nameof(Errors.Activity)));

        var activityDate = await _repository.GetActivityDate(createSignUp.ActivityDateId);

        if (activityDate == null) return new Result(Errors.General.NotFound(nameof(Errors.ActivityDate)));

        if (await AlreadySignedUp(createSignUp)) return new Result(Errors.ActivitySignUp.AlreadySignedUp(createSignUp.Email, activity.Name));

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

        return new Result();
    }

    private async Task<bool> AlreadySignedUp(CreateActivitySignUp createSignUp)
    {
        return await _repository.Exists(createSignUp.Email, createSignUp.ActivityId);
    }
}