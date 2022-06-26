using AcmeWidget.GetFit.Domain.Activities;
using Bogus;

namespace AcmeWidget.GetFit.Tests.Domain;

public class ActivityTests
{
    private readonly Faker _faker;

    public ActivityTests()
    {
        _faker = new Faker();
    }

    [Fact]
    public void AddDate_adds_date()
    {
        var activity = new Activity(_faker.Random.Word());

        var activityDate = ActivityDate.Build(DateTime.Now, DateTime.Now.AddDays(1), _faker.PickRandom<ActivityFrequency>(), activity).Value!;
        activity.AddDate(activityDate);

        Assert.Contains(activityDate, activity.ActivityDates);
    }
}