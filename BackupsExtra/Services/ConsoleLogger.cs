using System;

namespace BackupsExtra.Services
{
    public class ConsoleLogger : ILogger
    {
        public ConsoleLogger(bool messageWithDate)
        {
            WithDate = messageWithDate;
        }

        public bool WithDate { get; }
        public void DisplayMessage(string message, DateTime date)
        {
            if (WithDate)
            {
                Console.WriteLine($"{date} {message}");
            }
            else
            {
                Console.WriteLine(message);
            }
        }
    }
}