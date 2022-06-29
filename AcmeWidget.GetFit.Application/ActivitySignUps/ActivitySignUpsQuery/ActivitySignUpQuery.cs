using AcmeWidget.GetFit.Application.ActivitySignUps.Dtos;
using AcmeWidget.GetFit.Data.ActivitySignUpRepositories;

namespace AcmeWidget.GetFit.Application.ActivitySignUps.ActivitySignUpsQuery;

public class ActivitySignUpQuery : IActivitySignUpQuery
{
    private readonly IActivitySignUpRepository _repository;

    public ActivitySignUpQuery(IActivitySignUpRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ActivityFiltered>> Filtered(string? name, long? activityId, long? activityDateId)
    {
        var activities = await _repository.GetActivitiesWithSignUp(name, activityId, activityDateId);

        var activitiesFiltered = activities.OrderBy(p => p.Name).Select(p => new ActivityFiltered(
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

        return activitiesFiltered;
    }
}