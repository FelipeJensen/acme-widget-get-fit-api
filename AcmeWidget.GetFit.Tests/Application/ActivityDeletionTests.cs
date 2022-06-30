using AcmeWidget.GetFit.Application.Activities.UseCases.ActivitiesDeletion;
using AcmeWidget.GetFit.Data.ActivityRepositories;
using AcmeWidget.GetFit.Domain.Activities;
using AcmeWidget.GetFit.Domain.ResultHandling;
using AutoBogus;
using Bogus;
using Moq;

namespace AcmeWidget.GetFit.Tests.Application;

public class ActivityDeletionTests
{
    private readonly Faker _faker;

    public ActivityDeletionTests()
    {
        _faker = new Faker();
    }

    [Fact]
    public async Task With_non_existing_returns_not_found()
    {
        var activityRepoMock = new Mock<IActivityRepository>();

        var activityDeletion = new ActivityDeletion(activityRepoMock.Object);

        var result = await activityDeletion.Delete(_faker.Random.Long());

        Assert.False(result.Success);
        Assert.Equal(Errors.General.EntityNotFoundCode, result.Errors.Single().Code);
    }

    [Fact]
    public async Task Calls_repo_delete()
    {
        var activity = AutoFaker.Generate<Activity>();

        var activityRepoMock = new Mock<IActivityRepository>();
        activityRepoMock.Setup(p => p.Get(activity.Id)).ReturnsAsync(activity);

        var activityDeletion = new ActivityDeletion(activityRepoMock.Object);

        await activityDeletion.Delete(activity.Id);

        activityRepoMock.Verify(p => p.Delete(activity), Times.Once);
    }

    [Fact]
    public async Task Calls_repo_persist()
    {
        var activity = AutoFaker.Generate<Activity>();

        var activityRepoMock = new Mock<IActivityRepository>();
        activityRepoMock.Setup(p => p.Get(activity.Id)).ReturnsAsync(activity);

        var activityDeletion = new ActivityDeletion(activityRepoMock.Object);

        await activityDeletion.Delete(activity.Id);

        activityRepoMock.Verify(p => p.Persist(), Times.Once);
    }

    [Fact]
    public async Task Returns_success()
    {
        var activity = AutoFaker.Generate<Activity>();

        var activityRepoMock = new Mock<IActivityRepository>();
        activityRepoMock.Setup(p => p.Get(activity.Id)).ReturnsAsync(activity);

        var activityDeletion = new ActivityDeletion(activityRepoMock.Object);

        var result = await activityDeletion.Delete(activity.Id);

        Assert.True(result.Success);
    }
}