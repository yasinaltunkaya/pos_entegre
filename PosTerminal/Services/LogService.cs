using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PosTerminal.Services
{
    /// <summary>
    /// Thread-safe file logger writing under logs/yyyy-MM-dd.log.
    /// </summary>
    public sealed class LogService
    {
        private readonly string _logDirectory;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public LogService(string basePath)
        {
            _logDirectory = Path.Combine(basePath, "logs");
            Directory.CreateDirectory(_logDirectory);
        }

        public Task InfoAsync(string message, CancellationToken cancellationToken = default(CancellationToken))
        {
            return WriteAsync("INFO", message, cancellationToken);
        }

        public Task ErrorAsync(string message, CancellationToken cancellationToken = default(CancellationToken))
        {
            return WriteAsync("ERROR", message, cancellationToken);
        }

        private async Task WriteAsync(string level, string message, CancellationToken cancellationToken)
        {
            var filePath = Path.Combine(_logDirectory, string.Format("{0:yyyy-MM-dd}.log", DateTime.UtcNow));
            var line = string.Format("[{0:O}] [{1}] {2}{3}", DateTime.UtcNow, level, message, Environment.NewLine);

            await _semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                await File.AppendAllTextAsync(filePath, line, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                _semaphore.Release();
            }

        }
    }
}
