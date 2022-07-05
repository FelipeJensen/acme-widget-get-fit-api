using AcmeWidget.GetFit.Application.ActivitySignUps.Dtos;
using AcmeWidget.GetFit.Application.ActivitySignUps.UseCases.ActivitySignUpsCreation;
using AcmeWidget.GetFit.Application.ActivitySignUps.UseCases.ActivitySignUpsQuery;
using AcmeWidget.GetFit.Domain.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AcmeWidget.GetFit.Api.Controllers;

[ApiController]
public class ActivitySignUpsController : ControllerBase
{
    private readonly ILogger<ActivitySignUpsController> _logger;
    private readonly ErrorResponseBuilder _errorResponseBuilder;

    private readonly IActivitySignUpCreation _activitySignUpCreation;
    private readonly IActivitySignUpQuery _activitySignUpQuery;

    public ActivitySignUpsController(
        ILogger<ActivitySignUpsController> logger,
        ErrorResponseBuilder errorResponseBuilder,
        IActivitySignUpCreation activitySignUpCreation,
        IActivitySignUpQuery activitySignUpQuery
    )
    {
        _logger = logger;
        _errorResponseBuilder = errorResponseBuilder;
        _activitySignUpCreation = activitySignUpCreation;
        _activitySignUpQuery = activitySignUpQuery;
    }

    [HttpGet]
    [Route("activity-sign-ups")]
    [Produces(typeof(List<ActivityFiltered>))]
    public async Task<IActionResult> Get(string? name, long? activityId, long? activityDateId)
    {
        var activitySignUps = await _activitySignUpQuery.Filtered(name, activityId, activityDateId);

        return Ok(activitySignUps);
    }

    [HttpPost("activities/{id}/activity-sign-ups")]
    [Produces(typeof(long))]
    public async Task<IActionResult> Create(long id, CreateActivitySignUp createSignUp)
    {
        _logger.LogBody(createSignUp);

        var result = await _activitySignUpCreation.Create(createSignUp);

        if (result.Success)
        {
            return Ok(result.Value);
        }

        _logger.LogWarning("Failed to create activity sign up {errors}", result.Errors);

        return _errorResponseBuilder.Build(result, this);
    }
}