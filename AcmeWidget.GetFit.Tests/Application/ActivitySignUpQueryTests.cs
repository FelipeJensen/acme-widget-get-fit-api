using AcmeWidget.GetFit.Application.ActivitySignUps.Dtos;
using AcmeWidget.GetFit.Application.ActivitySignUps.UseCases.ActivitySignUpsQuery;
using AcmeWidget.GetFit.Data.ActivitySignUpRepositories;
using AcmeWidget.GetFit.Domain.Activities;
using Moq;
using Newtonsoft.Json;

namespace AcmeWidget.GetFit.Tests.Application;

public class ActivitySignUpQueryTests
{
    private readonly MockGenerator _mockGenerator;

    public ActivitySignUpQueryTests()
    {
        _mockGenerator = new MockGenerator();
    }

    [Fact]
    public async Task Build_activity_filtered()
    {
        var activity = _mockGenerator.FullActivity();

        var activitySignUpRepoMock = new Mock<IActivitySignUpRepository>();
        activitySignUpRepoMock.Setup(p => p.GetActivitiesWithSignUp(It.IsAny<string>(), It.IsAny<long>(), It.IsAny<long>()))
                              .ReturnsAsync(new List<Activity> { activity });

        var expected = new List<Activity> { activity }.OrderBy(p => p.Name).Select(p => new ActivityFiltered(
            p.Id,
            p.Name,
            p.ActivityDates.Where(k => k.ActivitySignUps.Any()).OrderBy(or => or.StartDate).Select(
                k => new ActivityDateFiltered(
                    k.Id,
                    k.StartDate,
                    k.EndDate,
                    k.Frequency,
                    k.ActivitySignUps.OrderBy(or => or.FirstName).ThenBy(p => p.LastName).Select(m =>
                        new ActivitySignUpFiltered(
                            m.Id,
                            m.FirstName,
                            m.LastName,
                            m.YearsOfExperienceInActivity,
                            m.Comments
                        )
                    ).ToList()
                )).ToList()
        )).ToList();

        var activitySignUpQuery = new ActivitySignUpQuery(activitySignUpRepoMock.Object);
        var activityFiltered = await activitySignUpQuery.Filtered(It.IsAny<string>(), It.IsAny<long>(), It.IsAny<long>());

        Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(activityFiltered));
    }

    [Fact]
    public async Task Should_call_repo_getActivitiesWithSignUp()
    {
        var activitySignUpRepoMock = new Mock<IActivitySignUpRepository>();
        activitySignUpRepoMock.Setup(p => p.GetActivitiesWithSignUp(It.IsAny<string>(), It.IsAny<long>(), It.IsAny<long>()))
                              .ReturnsAsync(new List<Activity>());

        var activitySignUpQuery = new ActivitySignUpQuery(activitySignUpRepoMock.Object);
        await activitySignUpQuery.Filtered(It.IsAny<string>(), It.IsAny<long>(), It.IsAny<long>());

        activitySignUpRepoMock.Verify(p => p.GetActivitiesWithSignUp(It.IsAny<string>(), It.IsAny<long>(), It.IsAny<long>()), Times.Once);
    }
}