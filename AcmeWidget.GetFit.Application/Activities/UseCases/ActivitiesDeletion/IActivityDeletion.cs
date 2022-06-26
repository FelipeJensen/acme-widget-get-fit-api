using AcmeWidget.GetFit.Domain.ResultHandling;

namespace AcmeWidget.GetFit.Application.Activities.UseCases.ActivitiesDeletion;

public interface IActivityDeletion
{
    Task<Result> Delete(long id);
}