using AcmeWidget.GetFit.Application.ActivitySignUps.Dtos;
using AcmeWidget.GetFit.Domain.ResultHandling;

namespace AcmeWidget.GetFit.Application.ActivitySignUps.UseCases.ActivitySignUpsCreation;

public interface IActivitySignUpCreation
{
    Task<Result<long>> Create(CreateActivitySignUp createSignUp);
}