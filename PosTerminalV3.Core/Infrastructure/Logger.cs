using System;
using System.IO;

namespace PosTerminalV3.Core.Infrastructure
{
    public static class Logger
    {
        public static void Log(string className, string methodName, string message)
        {
            var logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            Directory.CreateDirectory(logDirectory);

            var filePath = Path.Combine(logDirectory, DateTime.Now.ToString("yyyy-MM-dd") + ".log");
            var line = string.Format("{0:yyyy-MM-dd HH:mm:ss} | {1} | {2} | {3}", DateTime.Now, className, methodName, message);
            File.AppendAllText(filePath, line + Environment.NewLine);
        }
    }
}
