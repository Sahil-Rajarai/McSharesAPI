using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using McSharesAPI.Repository;
using McSharesAPI.Models;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using McSharesAPI.Services;
using Microsoft.AspNetCore.Http;

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
        public List<Log> GetAll() =>
            _logger.GetAllLogs();

    }
}