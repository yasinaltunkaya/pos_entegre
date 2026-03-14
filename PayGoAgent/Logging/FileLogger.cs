using System;
using System.IO;

namespace PayGoAgent.Logging
{
    public class FileLogger
    {
        private readonly string _logDirectory;
        private static readonly object Sync = new object();

        public FileLogger(string logDirectory)
        {
            _logDirectory = logDirectory;
            Directory.CreateDirectory(_logDirectory);
        }

        public void Info(string message)
        {
            Write("INFO", message);
        }

        public void Error(string message)
        {
            Write("ERROR", message);
        }

        public void Error(string message, Exception ex)
        {
            Write("ERROR", string.Format("{0} Exception: {1}", message, ex));
        }

        private void Write(string level, string message)
        {
            var filePath = Path.Combine(_logDirectory, string.Format("{0:yyyy-MM-dd}.log", DateTime.Now));
            var line = string.Format("{0:yyyy-MM-dd HH:mm:ss} [{1}] {2}", DateTime.Now, level, message);

            lock (Sync)
            {
                File.AppendAllText(filePath, line + Environment.NewLine);
            }
        }
    }
}
