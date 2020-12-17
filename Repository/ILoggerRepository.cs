using System;
using System.Collections.Generic;
using McSharesAPI.Models;

namespace McSharesAPI.Repository
{
    public interface ILoggerRepository
    {
        void LogError(string message, DateTime time);
        List<Log> GetAllLogs();
    }
}