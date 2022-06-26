using AcmeWidget.GetFit.Application.Activities.Dtos;
using AcmeWidget.GetFit.Application.Activities.UseCases.ActivitiesCreation;
using AcmeWidget.GetFit.Application.Activities.UseCases.ActivitiesDeletion;
using AcmeWidget.GetFit.Data;
using AcmeWidget.GetFit.Data.ActivityRepositories;
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

    private readonly IActivityRepository _repository;

    public ActivitiesController(
        ILogger<ActivitiesController> logger,
        IActivityCreation activityCreation,
        ErrorResponseBuilder errorResponseBuilder,
        IActivityDeletion activityDeletion,
        IActivityRepository repository
    )
    {
        _logger = logger;
        _errorResponseBuilder = errorResponseBuilder;
        _activityCreation = activityCreation;
        _activityDeletion = activityDeletion;
        _repository = repository;
    }

    [HttpGet("lookup")]
    [Produces(typeof(List<Lookup<long>>))]
    public IActionResult Lookup()
    {
        var lookup = _repository.Lookup();

        return Ok(lookup);
    }

    [HttpGet("{id}/dates/lookup")]
    [Produces(typeof(List<DateLookup>))]
    public IActionResult DatesLookup(long id)
    {
        var activityDates = _repository.DatesLookup(id);

        var lookups = activityDates.Select(p => new DateLookup(p.Id, p.StartDate, p.EndDate, p.Frequency));

        return Ok(lookups);
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

        return _errorResponseBuilder.Build(result, this);
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

        return _errorResponseBuilder.Build(result, this);
    }
}