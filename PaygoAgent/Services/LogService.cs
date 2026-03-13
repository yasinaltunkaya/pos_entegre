namespace PaygoAgent.Services;

/// <summary>
/// Persists operation logs to per-day files under logs/yyyy-MM-dd.log.
/// Thread-safe for concurrent API requests.
/// </summary>
public sealed class LogService
{
    private readonly string _logDirectory;
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public LogService(IHostEnvironment environment)
    {
        _logDirectory = Path.Combine(environment.ContentRootPath, "logs");
        Directory.CreateDirectory(_logDirectory);
    }

    public async Task InfoAsync(string message, CancellationToken cancellationToken = default) =>
        await WriteAsync("INFO", message, cancellationToken);

    public async Task ErrorAsync(string message, CancellationToken cancellationToken = default) =>
        await WriteAsync("ERROR", message, cancellationToken);

    private async Task WriteAsync(string level, string message, CancellationToken cancellationToken)
    {
        var filePath = Path.Combine(_logDirectory, $"{DateTime.UtcNow:yyyy-MM-dd}.log");
        var line = $"[{DateTime.UtcNow:O}] [{level}] {message}{Environment.NewLine}";

        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            await File.AppendAllTextAsync(filePath, line, cancellationToken);
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
