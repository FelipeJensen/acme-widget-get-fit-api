using AcmeWidget.GetFit.Application.Activities.Dtos;
using AcmeWidget.GetFit.Application.Activities.UseCases.ActivitiesCreation;
using AcmeWidget.GetFit.Data.ActivityRepositories;
using AcmeWidget.GetFit.Domain.ResultHandling;
using AutoBogus;
using Moq;

namespace AcmeWidget.GetFit.Tests.Application;

public class ActivityCreationTests
{
    private readonly Mock<IActivityRepository> _activityRepository;
    private const string Cycling = "Cycling";
    private readonly FakeGenerator _fakeGenerator;

    public ActivityCreationTests()
    {
        _fakeGenerator = new FakeGenerator();

        _activityRepository = new Mock<IActivityRepository>();
        _activityRepository.Setup(p => p.Exists(Cycling)).ReturnsAsync(true);
    }

    [Fact]
    public async Task With_empty_name_is_invalid()
    {
        var createActivity = AutoFaker.Generate<CreateActivity>();
        createActivity.Name = string.Empty;

        var activityCreation = new ActivityCreation(_activityRepository.Object);

        var result = await activityCreation.Create(createActivity);

        Assert.False(result.Success);
        Assert.Equal(Errors.Activity.ActivityNameEmptyCode, result.Error.Single().Code);
    }

    [Fact]
    public async Task With_name_that_already_exists_is_invalid()
    {
        var createActivity = AutoFaker.Generate<CreateActivity>();
        createActivity.Name = Cycling;

        var activityCreation = new ActivityCreation(_activityRepository.Object);

        var result = await activityCreation.Create(createActivity);

        Assert.False(result.Success);
        Assert.Equal(Errors.General.EntityAlreadyExists(nameof(Errors.Activity)).Code, result.Error.Single().Code);
    }

    [Fact]
    public async Task With_invalid_dates_is_invalid()
    {
        var createActivity = AutoFaker.Generate<CreateActivity>();
        createActivity.Dates.Clear();
        createActivity.Dates.Add(_fakeGenerator.InvalidCreateActivityDate);

        var activityCreation = new ActivityCreation(_activityRepository.Object);

        var result = await activityCreation.Create(createActivity);

        Assert.False(result.Success);
        Assert.Equal(Errors.ActivityDate.StartDateAfterEndDateCode, result.Error.First().Code);
    }

    [Fact]
    public async Task With_valid_activity_and_dates_success()
    {
        var createActivity = AutoFaker.Generate<CreateActivity>();
        createActivity.Dates.Clear();
        createActivity.Dates.Add(_fakeGenerator.CreateActivityDate);

        var activityCreation = new ActivityCreation(_activityRepository.Object);

        var result = await activityCreation.Create(createActivity);

        Assert.True(result.Success);
    }
}