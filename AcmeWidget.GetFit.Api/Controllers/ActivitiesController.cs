using AcmeWidget.GetFit.Application.Activities.Dtos;
using AcmeWidget.GetFit.Application.Activities.UseCases.ActivitiesCreation;
using AcmeWidget.GetFit.Domain.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AcmeWidget.GetFit.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ActivitiesController : ControllerBase
{
    private readonly ILogger<ActivitiesController> _logger;
    private readonly IActivityCreation _activityCreation;

    public ActivitiesController(IActivityCreation activityCreation, ILogger<ActivitiesController> logger)
    {
        _activityCreation = activityCreation;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(CreateActivity createActivity)
    {
        _logger.LogBody(createActivity);

        var result = await _activityCreation.Create(createActivity);

        if (result.Success)
        {
            return Ok();
        }

        foreach (var error in result.Error)
        {
            ModelState.AddModelError(error.Code, error.Message);
        }

        return BadRequest(ModelState);
    }
}