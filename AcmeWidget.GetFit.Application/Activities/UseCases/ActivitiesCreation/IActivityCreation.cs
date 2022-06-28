using AcmeWidget.GetFit.Application.Activities.Dtos;
using AcmeWidget.GetFit.Domain.ResultHandling;

namespace AcmeWidget.GetFit.Application.Activities.UseCases.ActivitiesCreation;

public interface IActivityCreation
{
    Task<Result<long>> Create(CreateActivity createActivity);
}