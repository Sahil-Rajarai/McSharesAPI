using System;
using System.Collections.Generic;
using McSharesAPI.Models;

namespace McSharesAPI.Repository
{
    public class InMemoryLoggerRepository : ILoggerRepository
    {
        public List<Log> logList = new List<Log>();
        public void LogError(string message, DateTime time)
        {
            var log = new Log
            {
                ErrorMessage = message,
                Time = time
            };

            logList.Add(log);
        }

        public List<Log> GetAllLogs()
        {
            return logList;
        }
    }
}