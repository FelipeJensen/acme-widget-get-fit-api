using AcmeWidget.GetFit.Application.Activities.UseCases.ActivitiesCreation;
using AcmeWidget.GetFit.Application.Activities.UseCases.ActivitiesDeletion;
using AcmeWidget.GetFit.Application.ActivitySignUps.UseCases.ActivitySignUpsCreation;
using AcmeWidget.GetFit.Application.ActivitySignUps.UseCases.ActivitySignUpsQuery;
using Microsoft.Extensions.DependencyInjection;

namespace AcmeWidget.GetFit.Application;

public static class Startup
{
    public static void AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddScoped<IActivityCreation, ActivityCreation>();
        services.AddScoped<IActivityDeletion, ActivityDeletion>();

        services.AddScoped<IActivitySignUpCreation, ActivitySignUpCreation>();
        services.AddScoped<IActivitySignUpQuery, ActivitySignUpQuery>();
    }
}