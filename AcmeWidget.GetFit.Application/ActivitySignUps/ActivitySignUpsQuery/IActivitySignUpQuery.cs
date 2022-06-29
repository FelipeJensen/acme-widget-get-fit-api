using AcmeWidget.GetFit.Application.ActivitySignUps.Dtos;

namespace AcmeWidget.GetFit.Application.ActivitySignUps.ActivitySignUpsQuery;

public interface IActivitySignUpQuery
{
    Task<List<ActivityFiltered>> Filtered(string? name, long? activityId, long? activityDateId);
}