using System;
using System.IO;
using System.Web;

namespace PosterMinalV2.Infrastructure
{
    public static class Logger
    {
        private static readonly object Sync = new object();

        public static void Info(string className, string methodName, string message)
        {
            Write(className, methodName, message, string.Empty);
        }

        public static void Error(string className, string methodName, string message, Exception ex)
        {
            Write(className, methodName, message, ex == null ? string.Empty : ex.ToString());
        }

        private static void Write(string className, string methodName, string message, string exception)
        {
            try
            {
                var folder = ConfigReader.Get("LogFolder", "Logs");
                var root = HttpContext.Current == null
                    ? AppDomain.CurrentDomain.BaseDirectory
                    : HttpContext.Current.Server.MapPath("~/");

                var logDirectory = Path.Combine(root, folder);
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                var filePath = Path.Combine(logDirectory, DateTime.Now.ToString("yyyy-MM-dd") + ".log");
                var line = string.Format(
                    "{0:yyyy-MM-dd HH:mm:ss.fff} | {1} | {2} | {3} | {4}",
                    DateTime.Now,
                    className,
                    methodName,
                    message,
                    exception);

                lock (Sync)
                {
                    File.AppendAllText(filePath, line + Environment.NewLine);
                }
            }
            catch
            {
                // Log hatası uygulamayı durdurmamalı.
            }
        }
    }
}
