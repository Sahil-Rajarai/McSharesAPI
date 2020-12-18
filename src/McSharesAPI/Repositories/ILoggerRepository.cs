using System.Collections.Generic;
using McSharesAPI.Models;

namespace McSharesAPI.Repositories
{
    public interface ILoggerRepository
    {
        void LogError(string message);
        IEnumerable<Log> GetAllLogs();
    }
}