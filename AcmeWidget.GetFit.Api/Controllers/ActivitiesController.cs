using AcmeWidget.GetFit.Application.Activities.Dtos;
using AcmeWidget.GetFit.Application.Activities.UseCases.ActivitiesCreation;
using AcmeWidget.GetFit.Application.Activities.UseCases.ActivitiesDeletion;
using AcmeWidget.GetFit.Domain.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AcmeWidget.GetFit.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ActivitiesController : ControllerBase
{
    private readonly ILogger<ActivitiesController> _logger;
    private readonly ErrorResponseBuilder _errorResponseBuilder;

    private readonly IActivityCreation _activityCreation;
    private readonly IActivityDeletion _activityDeletion;

    public ActivitiesController(
        ILogger<ActivitiesController> logger,
        IActivityCreation activityCreation,
        ErrorResponseBuilder errorResponseBuilder,
        IActivityDeletion activityDeletion)
    {
        _logger = logger;
        _errorResponseBuilder = errorResponseBuilder;
        _activityCreation = activityCreation;
        _activityDeletion = activityDeletion;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CreateActivity createActivity)
    {
        _logger.LogBody(createActivity);

        var result = await _activityCreation.Create(createActivity);

        if (result.Success)
        {
            return NoContent();
        }

        return _errorResponseBuilder.BadRequest(result, this);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id)
    {
        _logger.LogInformation("Deleting activity {id}", id);

        var result = await _activityDeletion.Delete(id);

        if (result.Success)
        {
            return NoContent();
        }

        return _errorResponseBuilder.BadRequest(result, this);
    }
}