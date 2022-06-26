using AcmeWidget.GetFit.Data;
using AcmeWidget.GetFit.Data.ActivityRepositories;
using AcmeWidget.GetFit.Domain.Activities;
using AutoBogus;
using Bogus;
using Moq;

namespace AcmeWidget.GetFit.Tests.Repositories;

public class ActivityRepositoryTests
{
    private readonly Faker _faker;
    private readonly FakeGenerator _fakeGenerator;

    public ActivityRepositoryTests()
    {
        _faker = new Faker();
        _fakeGenerator = new FakeGenerator();
    }

    [Fact]
    public void Exists_with_existing_entity_succeeds()
    {
        var activity = new Activity(_faker.Random.Word());

        var contextMock = new Mock<IGetFitDbContext>();
        contextMock.Setup(p => p.Query<Activity>()).Returns(new List<Activity> { activity }.AsQueryable);

        var activityRepository = new ActivityRepository(contextMock.Object);

        var actual = activityRepository.Exists(activity.Name);

        Assert.True(actual);
    }

    [Fact]
    public void Exists_with_non_existing_entity_fails()
    {
        var contextMock = new Mock<IGetFitDbContext>();
        contextMock.Setup(p => p.Query<Activity>()).Returns(new List<Activity>().AsQueryable);

        var activityRepository = new ActivityRepository(contextMock.Object);

        var actual = activityRepository.Exists(_faker.Random.Word());

        Assert.False(actual);
    }

    [Fact]
    public async Task Get_with_existing_entity_returns_entity()
    {
        var activity = new Activity(_faker.Random.Word());

        var contextMock = new Mock<IGetFitDbContext>();
        contextMock.Setup(p => p.FindAsync<Activity, long>(activity.Id)).ReturnsAsync(activity);

        var activityRepository = new ActivityRepository(contextMock.Object);

        var actual = await activityRepository.Get(activity.Id);

        Assert.Equal(activity, actual);
    }

    [Fact]
    public async Task Get_with_non_existing_entity_returns_null()
    {
        var activity = new Activity(_faker.Random.Word());

        var contextMock = new Mock<IGetFitDbContext>();
        contextMock.Setup(p => p.FindAsync<Activity, long>(activity.Id)).ReturnsAsync(() => null);

        var activityRepository = new ActivityRepository(contextMock.Object);

        var actual = await activityRepository.Get(activity.Id);

        Assert.Null(actual);
    }

    [Fact]
    public void Add_calls_add_once()
    {
        var contextMock = new Mock<IGetFitDbContext>();

        var activityRepository = new ActivityRepository(contextMock.Object);

        activityRepository.Add(AutoFaker.Generate<Activity>());

        contextMock.Verify(m => m.AddAsync(It.IsAny<Activity>()), Times.Once());
    }

    [Fact]
    public void Delete_calls_remove_once()
    {
        var contextMock = new Mock<IGetFitDbContext>();

        var activityRepository = new ActivityRepository(contextMock.Object);

        activityRepository.Delete(AutoFaker.Generate<Activity>());

        contextMock.Verify(m => m.Remove(It.IsAny<Activity>()), Times.Once());
    }

    [Fact]
    public void Persist_calls_saveChangesAsync_once()
    {
        var contextMock = new Mock<IGetFitDbContext>();

        var activityRepository = new ActivityRepository(contextMock.Object);

        activityRepository.Persist();

        contextMock.Verify(m => m.SaveChangesAsync(), Times.Once());
    }

    [Fact]
    public void Lookup_returns_existing_activities()
    {
        var activities = new List<Activity>()
        {
            AutoFaker.Generate<Activity>(),
            AutoFaker.Generate<Activity>(),
            AutoFaker.Generate<Activity>(),
        };

        var contextMock = new Mock<IGetFitDbContext>();
        contextMock.Setup(p => p.Query<Activity>()).Returns(activities.AsQueryable);

        var activityRepository = new ActivityRepository(contextMock.Object);

        var actual = activityRepository.Lookup();

        var expected = activities.Select(p => new Lookup<long>(p.Id, p.Name));

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ActivityDates_returns_existing_activityDates()
    {
        var activity = AutoFaker.Generate<Activity>();

        var contextMock = new Mock<IGetFitDbContext>();
        contextMock.Setup(p => p.Query<ActivityDate>()).Returns(activity.ActivityDates.AsQueryable);

        var activityRepository = new ActivityRepository(contextMock.Object);

        var actual = activityRepository.ActivityDates(activity.Id);

        Assert.Equal(activity.ActivityDates, actual);
    }

    [Fact]
    public async Task GetDate_with_existing_entity_returns_entity()
    {
        var activityDate = _fakeGenerator.ActivityDate;

        var contextMock = new Mock<IGetFitDbContext>();
        contextMock.Setup(p => p.FindAsync<ActivityDate, long>(activityDate.Id)).ReturnsAsync(activityDate);

        var activityRepository = new ActivityRepository(contextMock.Object);

        var actual = await activityRepository.GetDate(activityDate.Id);

        Assert.Equal(activityDate, actual);
    }

    [Fact]
    public async Task GetDate_with_non_existing_entity_returns_null()
    {
        var activityDate = _fakeGenerator.ActivityDate;

        var contextMock = new Mock<IGetFitDbContext>();
        contextMock.Setup(p => p.FindAsync<Activity, long>(activityDate.Id)).ReturnsAsync(() => null);

        var activityRepository = new ActivityRepository(contextMock.Object);

        var actual = await activityRepository.Get(activityDate.Id);

        Assert.Null(actual);
    }
}