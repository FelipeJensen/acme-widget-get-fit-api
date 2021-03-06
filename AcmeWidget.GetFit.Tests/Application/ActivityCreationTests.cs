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
    private readonly MockGenerator _mockGenerator;

    public ActivityCreationTests()
    {
        _mockGenerator = new MockGenerator();

        _activityRepository = new Mock<IActivityRepository>();
        _activityRepository.Setup(p => p.Exists(Cycling)).Returns(true);
    }

    [Fact]
    public async Task With_empty_name_is_invalid()
    {
        var createActivity = AutoFaker.Generate<CreateActivity>();
        createActivity.Name = string.Empty;

        var activityCreation = new ActivityCreation(_activityRepository.Object);

        var result = await activityCreation.Create(createActivity);

        Assert.False(result.Success);
        Assert.Equal(Errors.Activity.ActivityNameEmptyCode, result.Errors.Single().Code);
    }

    [Fact]
    public async Task With_name_that_already_exists_is_invalid()
    {
        var createActivity = AutoFaker.Generate<CreateActivity>();
        createActivity.Name = Cycling;

        var activityCreation = new ActivityCreation(_activityRepository.Object);

        var result = await activityCreation.Create(createActivity);

        Assert.False(result.Success);
        Assert.Equal(Errors.General.EntityAlreadyExists(nameof(Errors.Activity)).Code, result.Errors.Single().Code);
    }

    [Fact]
    public async Task With_invalid_dates_is_invalid()
    {
        var createActivity = AutoFaker.Generate<CreateActivity>();
        createActivity.Dates.Clear();
        createActivity.Dates.Add(_mockGenerator.InvalidCreateActivityDate);

        var activityCreation = new ActivityCreation(_activityRepository.Object);

        var result = await activityCreation.Create(createActivity);

        Assert.False(result.Success);
        Assert.Equal(Errors.ActivityDate.StartDateAfterEndDateCode, result.Errors.First().Code);
    }

    [Fact]
    public async Task With_valid_activity_and_dates_success()
    {
        var createActivity = AutoFaker.Generate<CreateActivity>();
        createActivity.Dates.Clear();
        createActivity.Dates.Add(_mockGenerator.CreateActivityDate);

        var activityCreation = new ActivityCreation(_activityRepository.Object);

        var result = await activityCreation.Create(createActivity);

        Assert.True(result.Success);
    }
}