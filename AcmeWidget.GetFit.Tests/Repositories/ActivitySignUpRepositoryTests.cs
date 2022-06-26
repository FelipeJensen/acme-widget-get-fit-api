using AcmeWidget.GetFit.Data;
using AcmeWidget.GetFit.Data.ActivitySignUpRepositories;
using AcmeWidget.GetFit.Domain.ActivitySignups;
using AutoBogus;
using Moq;

namespace AcmeWidget.GetFit.Tests.Repositories;

public class ActivitySignUpRepositoryTests
{
    [Fact]
    public void Exists_with_existing_entity_succeeds()
    {
        var activitySignUp = AutoFaker.Generate<ActivitySignUp>();

        var contextMock = new Mock<IGetFitDbContext>();
        contextMock.Setup(p => p.Query<ActivitySignUp>()).Returns(new List<ActivitySignUp> { activitySignUp }.AsQueryable);

        var activityRepository = new ActivitySignUpRepository(contextMock.Object);

        var actual = activityRepository.Exists(activitySignUp.Email, activitySignUp.ActivityId);

        Assert.True(actual);
    }

    [Fact]
    public void Exists_with_non_existing_entity_fails()
    {
        var contextMock = new Mock<IGetFitDbContext>();
        contextMock.Setup(p => p.Query<ActivitySignUp>()).Returns(new List<ActivitySignUp>().AsQueryable);

        var activityRepository = new ActivitySignUpRepository(contextMock.Object);

        var activitySignUp = AutoFaker.Generate<ActivitySignUp>();
        var actual = activityRepository.Exists(activitySignUp.Email, activitySignUp.ActivityId);

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
}