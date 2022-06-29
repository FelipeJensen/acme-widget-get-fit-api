using AcmeWidget.GetFit.Application.ActivitySignUps.ActivitySignUpsCreation;
using AcmeWidget.GetFit.Application.ActivitySignUps.Dtos;
using AcmeWidget.GetFit.Data.ActivityRepositories;
using AcmeWidget.GetFit.Data.ActivitySignUpRepositories;
using AcmeWidget.GetFit.Domain.Activities;
using AcmeWidget.GetFit.Domain.ActivitySignups;
using AcmeWidget.GetFit.Domain.ResultHandling;
using AutoBogus;
using Moq;

namespace AcmeWidget.GetFit.Tests.Application;

public class ActivitySignUpCreationTests
{
    private readonly Mock<IActivitySignUpRepository> _signUpRepoMock;
    private readonly Mock<IActivityRepository> _activityRepoMock;
    private readonly MockGenerator _mockGenerator;

    public ActivitySignUpCreationTests()
    {
        _signUpRepoMock = new Mock<IActivitySignUpRepository>();
        _activityRepoMock = new Mock<IActivityRepository>();
        _mockGenerator = new MockGenerator();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task With_empty_firstName_is_invalid(string firstName)
    {
        var createActivitySignUp = AutoFaker.Generate<CreateActivitySignUp>();
        createActivitySignUp.FirstName = firstName;

        var activitySignUpCreation = new ActivitySignUpCreation(_signUpRepoMock.Object, _activityRepoMock.Object);

        var result = await activitySignUpCreation.Create(createActivitySignUp);

        Assert.False(result.Success);
        Assert.Equal(Errors.General.RequiredCode, result.Errors.Single().Code);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task With_empty_lastName_is_invalid(string lastName)
    {
        var createActivitySignUp = AutoFaker.Generate<CreateActivitySignUp>();
        createActivitySignUp.LastName = lastName;

        var activitySignUpCreation = new ActivitySignUpCreation(_signUpRepoMock.Object, _activityRepoMock.Object);

        var result = await activitySignUpCreation.Create(createActivitySignUp);

        Assert.False(result.Success);
        Assert.Equal(Errors.General.RequiredCode, result.Errors.Single().Code);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task With_empty_email_is_invalid(string email)
    {
        var createActivitySignUp = AutoFaker.Generate<CreateActivitySignUp>();
        createActivitySignUp.Email = email;

        var activitySignUpCreation = new ActivitySignUpCreation(_signUpRepoMock.Object, _activityRepoMock.Object);

        var result = await activitySignUpCreation.Create(createActivitySignUp);

        Assert.False(result.Success);
        Assert.Equal(Errors.General.RequiredCode, result.Errors.Single().Code);
    }

    [Fact]
    public async Task With_non_existing_activity_is_invalid()
    {
        var createActivitySignUp = AutoFaker.Generate<CreateActivitySignUp>();
        _activityRepoMock.Setup(p => p.Get(It.IsAny<long>())).ReturnsAsync((Activity?)null);

        var activitySignUpCreation = new ActivitySignUpCreation(_signUpRepoMock.Object, _activityRepoMock.Object);

        var result = await activitySignUpCreation.Create(createActivitySignUp);

        Assert.False(result.Success);
        Assert.Equal(Errors.General.EntityNotFoundCode, result.Errors.Single().Code);
    }

    [Fact]
    public async Task With_non_existing_activityDate_is_invalid()
    {
        var createActivitySignUp = AutoFaker.Generate<CreateActivitySignUp>();
        _activityRepoMock.Setup(p => p.Get(It.IsAny<long>())).ReturnsAsync(AutoFaker.Generate<Activity>());
        _activityRepoMock.Setup(p => p.GetDate(It.IsAny<long>())).ReturnsAsync((ActivityDate?)null);

        var activitySignUpCreation = new ActivitySignUpCreation(_signUpRepoMock.Object, _activityRepoMock.Object);

        var result = await activitySignUpCreation.Create(createActivitySignUp);

        Assert.False(result.Success);
        Assert.Equal(Errors.General.EntityNotFoundCode, result.Errors.Single().Code);
    }

    [Fact]
    public async Task When_already_sign_up_is_invalid()
    {
        var activity = AutoFaker.Generate<Activity>();

        var activityDate = _mockGenerator.ActivityDate;

        _activityRepoMock.Setup(p => p.Get(activity.Id)).ReturnsAsync(activity);
        _activityRepoMock.Setup(p => p.GetDate(activityDate.Id)).ReturnsAsync(activityDate);

        var createActivitySignUp = AutoFaker.Generate<CreateActivitySignUp>();
        createActivitySignUp.ActivityId = activity.Id;
        createActivitySignUp.ActivityDateId = activityDate.Id;

        _signUpRepoMock.Setup(p => p.Exists(createActivitySignUp.Email, createActivitySignUp.ActivityDateId)).Returns(true);

        var activitySignUpCreation = new ActivitySignUpCreation(_signUpRepoMock.Object, _activityRepoMock.Object);

        var result = await activitySignUpCreation.Create(createActivitySignUp);

        Assert.False(result.Success);
        Assert.Equal(Errors.ActivitySignUp.AlreadySignedUpCode, result.Errors.Single().Code);
    }

    [Fact]
    public async Task Calls_repository_adds()
    {
        var activity = AutoFaker.Generate<Activity>();
        activity.ActivityDates.Clear();

        var activityDate = _mockGenerator.ActivityDate;

        var createActivitySignUp = AutoFaker.Generate<CreateActivitySignUp>();
        createActivitySignUp.ActivityId = activity.Id;
        createActivitySignUp.ActivityDateId = activityDate.Id;

        _signUpRepoMock.Setup(p => p.Exists(createActivitySignUp.Email, createActivitySignUp.ActivityDateId)).Returns(false);

        _activityRepoMock.Setup(p => p.Get(activity.Id)).ReturnsAsync(activity);
        _activityRepoMock.Setup(p => p.GetDate(activityDate.Id)).ReturnsAsync(activityDate);

        var activitySignUpCreation = new ActivitySignUpCreation(_signUpRepoMock.Object, _activityRepoMock.Object);

        await activitySignUpCreation.Create(createActivitySignUp);

        _signUpRepoMock.Verify(p => p.Add(It.IsAny<ActivitySignUp>()), Times.Once());
    }

    [Fact]
    public async Task Calls_repository_persist()
    {
        var activity = AutoFaker.Generate<Activity>();
        activity.ActivityDates.Clear();

        var activityDate = _mockGenerator.ActivityDate;

        var createActivitySignUp = AutoFaker.Generate<CreateActivitySignUp>();
        createActivitySignUp.ActivityId = activity.Id;
        createActivitySignUp.ActivityDateId = activityDate.Id;

        _signUpRepoMock.Setup(p => p.Exists(createActivitySignUp.Email, createActivitySignUp.ActivityDateId)).Returns(false);

        _activityRepoMock.Setup(p => p.Get(activity.Id)).ReturnsAsync(activity);
        _activityRepoMock.Setup(p => p.GetDate(activityDate.Id)).ReturnsAsync(activityDate);

        var activitySignUpCreation = new ActivitySignUpCreation(_signUpRepoMock.Object, _activityRepoMock.Object);

        await activitySignUpCreation.Create(createActivitySignUp);

        _signUpRepoMock.Verify(p => p.Persist(), Times.Once());
    }
}