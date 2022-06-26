using AcmeWidget.GetFit.Domain.Activities;
using AcmeWidget.GetFit.Domain.ResultHandling;
using Bogus;

namespace AcmeWidget.GetFit.Tests.Domain;

public class ActivityDateTests
{
    private readonly Faker _faker;

    public ActivityDateTests()
    {
        _faker = new Faker();
    }

    [Theory]
    [InlineData(ActivityFrequency.Daily)]
    [InlineData(ActivityFrequency.Weekly)]
    [InlineData(ActivityFrequency.Monthly)]
    public void Build_with_endDate_should_ignore_frequency(ActivityFrequency frequency)
    {
        var activity = new Activity(_faker.Random.Word());

        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        var activityDate = ActivityDate.Build(startDate, endDate, frequency, activity).Value!;

        Assert.Equal(ActivityFrequency.Period, activityDate.Frequency);
    }

    [Fact]
    public void Build_with_endDate_greater_than_startDate_is_invalid()
    {
        var activity = new Activity(_faker.Random.Word());

        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(-1);

        var result = ActivityDate.Build(startDate, endDate, _faker.PickRandom(ActivityFrequency.Daily, ActivityFrequency.Weekly, ActivityFrequency.Monthly), activity);

        Assert.False(result.Success);
        Assert.Equal(Errors.ActivityDate.StartDateAfterEndDateCode, result.Errors.Single().Code);
    }

    [Fact]
    public void Build_with_frequency_period_with_endDate_is_invalid()
    {
        var activity = new Activity(_faker.Random.Word());

        var startDate = DateTime.Now;

        var result = ActivityDate.Build(startDate, null, ActivityFrequency.Period, activity);

        Assert.False(result.Success);
        Assert.Equal(Errors.ActivityDate.PeriodFrequencyWithoutEndDateCode, result.Errors.Single().Code);
    }
}