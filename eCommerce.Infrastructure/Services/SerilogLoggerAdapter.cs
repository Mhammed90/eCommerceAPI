using eCommerce.Application.Services.Logging;
using Microsoft.Extensions.Logging;

namespace eCommerce.Infrastructure.Services;

public class SerilogLoggerAdapter<T> : IAppLogger<T>
{
    private readonly ILogger<T> _logger;

    public SerilogLoggerAdapter(ILogger<T> logger)
    {
        _logger = logger;
    }

    public void LogInformation(string message) => _logger.LogInformation(message);

    public void LogWarning(string message) => _logger.LogWarning(message);

    public void LogError(Exception exception, string message) => _logger.LogError(exception, message);
}