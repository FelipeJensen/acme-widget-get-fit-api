using Microsoft.Extensions.Logging;

namespace AcmeWidget.GetFit.Domain.Extensions;

// ReSharper disable once InconsistentNaming
public static class ILoggerExtensions
{
    public static void LogBody<T>(this ILogger logger, T body)
    {
        logger.LogInformation("Request body: {@body}", body);
    }
}