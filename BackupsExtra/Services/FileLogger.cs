using System;
using System.IO;

namespace BackupsExtra.Services
{
    public class FileLogger : ILogger
    {
        public FileLogger(string filePath, bool messageWithDate)
        {
            FilePath = filePath;
            WithDate = messageWithDate;
        }

        public string FilePath { get; }
        public bool WithDate { get; }
        public void DisplayMessage(string message, DateTime date)
        {
            var sw = new StreamWriter(FilePath);
            if (WithDate)
            {
                sw.WriteLine($"{date} {message}");
            }
            else
            {
                sw.WriteLine(message);
            }
        }
    }
}