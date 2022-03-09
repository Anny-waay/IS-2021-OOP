using System;

namespace BackupsExtra.Services
{
    public interface ILogger
    {
        void DisplayMessage(string message, DateTime date);
    }
}