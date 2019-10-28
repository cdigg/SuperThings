using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SuperThings.Data.Models;
using SuperThings.Logic.Services;

namespace SuperThings.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonitoringController : ControllerBase
    {
        private readonly ILogger<RegistrationController> _logger;
        private readonly IMonitoringService _monitoringService;

        public MonitoringController(ILogger<RegistrationController> logger, IMonitoringService monitoringService)
        {
            _logger = logger;
            _monitoringService = monitoringService;
        }

        [HttpGet]
        [Route("count")]
        public ActionResult GetCount()
        {
            _logger.LogDebug($"Get total registration count");
            var count =  _monitoringService.Count().Result;
            return Ok(count);
        }

        [HttpGet]
        [Route("favorites")]
        public ActionResult GetFavorites([FromQuery] int? count = 10)
        {
            _logger.LogDebug($"Get favorite integers. Count: {count}");
            var favorites = _monitoringService.Favorites(count.Value).Result;
            return Ok(favorites);
        }


    }
}
