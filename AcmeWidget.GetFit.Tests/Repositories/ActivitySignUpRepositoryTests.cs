using AcmeWidget.GetFit.Data;
using AcmeWidget.GetFit.Data.ActivitySignUpRepositories;
using AcmeWidget.GetFit.Domain.Activities;
using AcmeWidget.GetFit.Domain.ActivitySignups;
using AutoBogus;
using Bogus;
using MockQueryable.Moq;
using Moq;

namespace AcmeWidget.GetFit.Tests.Repositories;

public class ActivitySignUpRepositoryTests
{
    private readonly MockGenerator _mockGenerator;
    private readonly Faker _faker;

    public ActivitySignUpRepositoryTests()
    {
        _mockGenerator = new MockGenerator();
        _faker = new Faker();
    }

    [Fact]
    public void Exists_with_existing_entity_succeeds()
    {
        var activitySignUp = AutoFaker.Generate<ActivitySignUp>();

        var contextMock = new Mock<IGetFitDbContext>();
        contextMock.Setup(p => p.Query<ActivitySignUp>()).Returns(new List<ActivitySignUp> { activitySignUp }.AsQueryable);

        var activityRepository = new ActivitySignUpRepository(contextMock.Object);

        var actual = activityRepository.Exists(activitySignUp.Email, activitySignUp.ActivityDateId);

        Assert.True(actual);
    }

    [Fact]
    public void Exists_with_non_existing_entity_fails()
    {
        var contextMock = new Mock<IGetFitDbContext>();
        contextMock.Setup(p => p.Query<ActivitySignUp>()).Returns(new List<ActivitySignUp>().AsQueryable);

        var activityRepository = new ActivitySignUpRepository(contextMock.Object);

        var activitySignUp = AutoFaker.Generate<ActivitySignUp>();
        var actual = activityRepository.Exists(activitySignUp.Email, activitySignUp.ActivityDateId);

        Assert.False(actual);
    }

    [Fact]
    public async Task Get_with_existing_entity_returns_entity()
    {
        var activitySignUp = AutoFaker.Generate<ActivitySignUp>();

        var contextMock = new Mock<IGetFitDbContext>();
        contextMock.Setup(p => p.FindAsync<ActivitySignUp, long>(activitySignUp.Id)).ReturnsAsync(activitySignUp);

        var activitySignUpRepository = new ActivitySignUpRepository(contextMock.Object);

        var actual = await activitySignUpRepository.Get(activitySignUp.Id);

        Assert.Equal(activitySignUp, actual);
    }

    [Fact]
    public async Task Get_with_non_existing_entity_returns_null()
    {
        var activitySignUp = AutoFaker.Generate<ActivitySignUp>();

        var contextMock = new Mock<IGetFitDbContext>();
        contextMock.Setup(p => p.FindAsync<ActivitySignUp, long>(activitySignUp.Id)).ReturnsAsync(() => null);

        var activitySignUpRepository = new ActivitySignUpRepository(contextMock.Object);

        var actual = await activitySignUpRepository.Get(activitySignUp.Id);

        Assert.Null(actual);
    }

    [Fact]
    public void Add_calls_add_once()
    {
        var contextMock = new Mock<IGetFitDbContext>();

        var activityRepository = new ActivitySignUpRepository(contextMock.Object);

        activityRepository.Add(AutoFaker.Generate<ActivitySignUp>());

        contextMock.Verify(m => m.AddAsync(It.IsAny<ActivitySignUp>()), Times.Once());
    }

    [Fact]
    public void Persist_calls_saveChangesAsync_once()
    {
        var contextMock = new Mock<IGetFitDbContext>();

        var activityRepository = new ActivitySignUpRepository(contextMock.Object);

        activityRepository.Persist();

        contextMock.Verify(m => m.SaveChangesAsync(), Times.Once());
    }

    [Fact]
    public async Task GetActivitiesWithSignUp_filters_by_firstName()
    {
        var expected = _faker.Name.FirstName();

        var ac1 = _mockGenerator.FullActivity();

        var activity = _mockGenerator.FullActivity();
        ac1.ActivitySignUps.Add(new ActivitySignUp(expected, _faker.Random.Word(), _faker.Internet.Email(), activity, activity.ActivityDates.First(), null, null));

        var ac2 = _mockGenerator.FullActivity();

        var contextMock = new Mock<IGetFitDbContext>();
        var activities = new List<Activity> { ac1, ac2 };

        contextMock.Setup(p => p.Query<Activity>()).Returns(activities.BuildMock());

        var activitySignUpRepository = new ActivitySignUpRepository(contextMock.Object);
        var activitiesWithSignUp = await activitySignUpRepository.GetActivitiesWithSignUp(expected, null, null);

        Assert.NotNull(activitiesWithSignUp.Single(p => p.ActivitySignUps.SingleOrDefault(k => k.FirstName == expected) != null));
    }

    [Fact]
    public async Task GetActivitiesWithSignUp_filters_by_lastName()
    {
        var expected = _faker.Name.LastName();

        var ac1 = _mockGenerator.FullActivity();

        var activity = _mockGenerator.FullActivity();
        ac1.ActivitySignUps.Add(new ActivitySignUp(_faker.Random.Word(), expected, _faker.Internet.Email(), activity, activity.ActivityDates.First(), null, null));

        var ac2 = _mockGenerator.FullActivity();

        var contextMock = new Mock<IGetFitDbContext>();
        var activities = new List<Activity> { ac1, ac2 };

        contextMock.Setup(p => p.Query<Activity>()).Returns(activities.BuildMock());

        var activitySignUpRepository = new ActivitySignUpRepository(contextMock.Object);
        var activitiesWithSignUp = await activitySignUpRepository.GetActivitiesWithSignUp(expected, null, null);

        Assert.NotNull(activitiesWithSignUp.Single(p => p.ActivitySignUps.SingleOrDefault(k => k.LastName == expected) != null));
    }

    [Fact]
    public async Task GetActivitiesWithSignUp_filters_by_activity()
    {
        var ac1 = _mockGenerator.FullActivity();

        var activity = _mockGenerator.FullActivity();
        ac1.ActivitySignUps.Add(new ActivitySignUp(_faker.Random.Word(), _faker.Name.LastName(), _faker.Internet.Email(), activity, activity.ActivityDates.First(), null, null));

        var ac2 = _mockGenerator.FullActivity();

        var contextMock = new Mock<IGetFitDbContext>();
        var activities = new List<Activity> { ac1, ac2 };

        contextMock.Setup(p => p.Query<Activity>()).Returns(activities.BuildMock());

        var activitySignUpRepository = new ActivitySignUpRepository(contextMock.Object);
        var activitiesWithSignUp = await activitySignUpRepository.GetActivitiesWithSignUp(null, ac2.Id, null);

        Assert.NotNull(activitiesWithSignUp.Single(p => p.Id == ac2.Id));
    }

    [Fact]
    public async Task GetActivitiesWithSignUp_filters_by_activityDate()
    {
        var ac1 = _mockGenerator.FullActivity();

        var activity = _mockGenerator.FullActivity();
        ac1.ActivitySignUps.Add(new ActivitySignUp(_faker.Random.Word(), _faker.Name.LastName(), _faker.Internet.Email(), activity, activity.ActivityDates.First(), null, null));

        var ac2 = _mockGenerator.FullActivity();

        var contextMock = new Mock<IGetFitDbContext>();
        var activities = new List<Activity> { ac1, ac2 };

        contextMock.Setup(p => p.Query<Activity>()).Returns(activities.BuildMock());

        var activitySignUpRepository = new ActivitySignUpRepository(contextMock.Object);
        var activitiesWithSignUp = await activitySignUpRepository.GetActivitiesWithSignUp(null, null, ac2.ActivityDates.First().Id);

        Assert.NotNull(activitiesWithSignUp.Single(p => p.ActivityDates.First().Id == ac2.ActivityDates.First().Id));
    }
}