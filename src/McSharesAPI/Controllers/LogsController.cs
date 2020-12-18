using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using McSharesAPI.Models;
using McSharesAPI.Repositories;

namespace McSharesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LogsController : ControllerBase
    {
        private ILoggerRepository _logger;
        public LogsController(ILoggerRepository logger)
        {
            _logger = logger;
        }

        // GET: api/Logs
        //Get all Error Logs
        [HttpGet]
        public IEnumerable<Log> GetAll() =>
            _logger.GetAllLogs();

    }
}