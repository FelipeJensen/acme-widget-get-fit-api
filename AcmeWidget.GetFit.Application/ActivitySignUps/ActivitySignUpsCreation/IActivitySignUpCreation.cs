using AcmeWidget.GetFit.Application.ActivitySignUps.Dtos;
using AcmeWidget.GetFit.Domain.ResultHandling;

namespace AcmeWidget.GetFit.Application.ActivitySignUps.ActivitySignUpsCreation;

public interface IActivitySignUpCreation
{
    Task<Result> Create(CreateActivitySignUp createSignUp);
}