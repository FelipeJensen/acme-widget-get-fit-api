using AcmeWidget.GetFit.Application.Activities.Dtos;
using AcmeWidget.GetFit.Domain.Activities;
using AutoBogus;
using Bogus;

namespace AcmeWidget.GetFit.Tests;

public class MockGenerator
{
    private readonly Faker _faker = new();

    public CreateActivityDate InvalidCreateActivityDate => new()
    {
        StartDate = DateTime.Now,
        EndDate = DateTime.Now.AddDays(-1),
        Frequency = _faker.PickRandom<ActivityFrequency>()
    };

    public CreateActivityDate CreateActivityDate => new()
    {
        StartDate = DateTime.Now,
        EndDate = DateTime.Now.AddDays(1),
        Frequency = _faker.PickRandom<ActivityFrequency>()
    };

    public ActivityDate ActivityDateMock()
    {
        var activityDate = ActivityDate.Build(
                                           DateTime.Now,
                                           DateTime.Now.AddDays(1),
                                           _faker.PickRandom<ActivityFrequency>(),
                                           AutoFaker.Generate<Activity>())
                                       .Value!;

        var id = activityDate.GetType().GetProperty(nameof(activityDate.Id));
        id!.SetValue(activityDate, _faker.Random.Long());

        return activityDate;
    }

    public ActivityDate ActivityDateMock(Activity activity)
    {
        var activityDate = ActivityDate.Build(
                                           DateTime.Now,
                                           DateTime.Now.AddDays(1),
                                           _faker.PickRandom<ActivityFrequency>(),
                                           activity)
                                       .Value!;

        var id = activityDate.GetType().GetProperty(nameof(activityDate.Id));
        id!.SetValue(activityDate, _faker.Random.Long());

        var activityId = activityDate.GetType().GetProperty(nameof(activityDate.ActivityId));
        activityId!.SetValue(activityDate, activity.Id);

        return activityDate;
    }

    public Activity FullActivity()
    {
        var activity = AutoFaker.Generate<Activity>();

        var activityDate = ActivityDateMock(activity);
        activityDate.ActivitySignUps = activity.ActivitySignUps;

        activity.ActivityDates = new List<ActivityDate> { activityDate };

        return activity;
    }
}