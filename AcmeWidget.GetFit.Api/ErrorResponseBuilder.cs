using AcmeWidget.GetFit.Domain.ResultHandling;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AcmeWidget.GetFit.Api;

public class ErrorResponseBuilder
{
    private readonly IOptions<ApiBehaviorOptions> _options;

    public ErrorResponseBuilder(IOptions<ApiBehaviorOptions> options)
    {
        _options = options;
    }

    public IActionResult BadRequest(Result result, ControllerBase controller)
    {
        if (result.Errors.Count == 1 && result.Errors.First().Code == Errors.General.NotFound(string.Empty).Code)
        {
            return controller.NotFound();
        }

        foreach (var error in result.Errors)
        {
            controller.ModelState.AddModelError(error.Code, error.Message);
        }

        return _options.Value.InvalidModelStateResponseFactory(controller.ControllerContext);
    }
}