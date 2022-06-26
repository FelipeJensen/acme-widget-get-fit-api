using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AcmeWidget.GetFit.Api;

public class BadRequestGenerator
{
    private readonly IOptions<ApiBehaviorOptions> _options;

    public BadRequestGenerator(IOptions<ApiBehaviorOptions> options)
    {
        _options = options;
    }

    public IActionResult BadRequest(ControllerContext controllerContext) => _options.Value.InvalidModelStateResponseFactory(controllerContext);
}