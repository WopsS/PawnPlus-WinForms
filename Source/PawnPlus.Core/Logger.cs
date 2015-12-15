using System;
using System.IO;

namespace PawnPlus.Core
{
    public enum LogType
    {
        Debug,
        Error,
        Warning
    }

    public static class Logger
    {
        private static string filePath = Path.Combine(ApplicationData.MyDocumentsPath, "application.log");

        static Logger()
        {
            // Check if we already have a log file.
            if (File.Exists(filePath) == true)
            {
                // Delete the old log file.
                File.Delete(filePath);
            }
        }

        public static void Write(LogType type, string format, params string[] parameters)
        {
            if (Directory.Exists(ApplicationData.MyDocumentsPath) == false)
            {
                Directory.CreateDirectory(ApplicationData.MyDocumentsPath);
            }

            // Formata our message with parameters.
            string result = string.Format("[{0} | {1}] {2}", DateTime.Now.ToString(), type.ToString().ToUpper(), string.Format(format, parameters));
        
            File.AppendAllText(filePath, result + Environment.NewLine);
        }

        public static void Write(Exception ex)
        {
            Write(LogType.Error, "{0}{1}{2}", ex.Message, Environment.NewLine, ex.ToString());
        }
    }
}
