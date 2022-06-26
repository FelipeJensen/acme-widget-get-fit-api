using AcmeWidget.GetFit.Application.ActivitySignUps.ActivitySignUpsCreation;
using AcmeWidget.GetFit.Application.ActivitySignUps.Dtos;
using AcmeWidget.GetFit.Domain.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AcmeWidget.GetFit.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ActivitySignUpsController : ControllerBase
{
    private readonly ILogger<ActivitySignUpsController> _logger;
    private readonly ErrorResponseBuilder _errorResponseBuilder;

    private readonly IActivitySignUpCreation _activitySignUpCreation;

    public ActivitySignUpsController(
        ILogger<ActivitySignUpsController> logger,
        ErrorResponseBuilder errorResponseBuilder,
        IActivitySignUpCreation activitySignUpCreation
    )
    {
        _logger = logger;
        _errorResponseBuilder = errorResponseBuilder;
        _activitySignUpCreation = activitySignUpCreation;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateActivitySignUp createSignUp)
    {
        _logger.LogBody(createSignUp);

        var result = await _activitySignUpCreation.Create(createSignUp);

        if (result.Success)
        {
            return Ok(createSignUp.ActivityId);
        }

        _logger.LogWarning("Failed to create activity sign up {errors}", result.Errors);

        return _errorResponseBuilder.Build(result, this);
    }
}