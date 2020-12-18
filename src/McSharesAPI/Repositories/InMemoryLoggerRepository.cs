using System;
using System.Collections.Generic;
using McSharesAPI.Models;

namespace McSharesAPI.Repositories
{
    public class InMemoryLoggerRepository : ILoggerRepository
    {
        private List<Log> _logList = new List<Log>();

        public void LogError(string message)
        {
            var log = new Log
            {
                ErrorMessage = message,
                Time = DateTime.UtcNow
            };

            _logList.Add(log);
        }

        public IEnumerable<Log> GetAllLogs()
        {
            return _logList.AsReadOnly();
        }
    }
}