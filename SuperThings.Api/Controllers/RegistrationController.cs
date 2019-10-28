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
    public class RegistrationController : ControllerBase
    {
        private readonly ILogger<RegistrationController> _logger;
        private readonly IRegistrationService _registrationService;

        public RegistrationController(ILogger<RegistrationController> logger, IRegistrationService registrationService)
        {
            _logger = logger;
            _registrationService = registrationService;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetRegistrant(int id)
        {
            var entry = _registrationService.GetRegistrant(id).Result;
            if (entry == null)
            {
                return NotFound();
            }
            return Ok(entry);
        }

        [HttpGet]
        [Route("recent")]
        public ActionResult GetMostRecent([FromQuery] int? count = 10)
        {
            _logger.LogInformation($"Get most recent {count}");
            var list = _registrationService.GetMostRecent(count.Value).Result;
            return Ok(list);
        }

        [HttpPost]
        public ActionResult Register([FromBody] RegistrantVm entry)
        {
            _logger.LogInformation($"Registration received for {entry.Email}");

            //complete the registration
            entry.Id = _registrationService.Register(entry.FullName, entry.Email, entry.FavoriteFiveIntegers, entry.DateOfBirth.Value, entry.EmailOptIn.Value, entry.TimeOfRegistration.Value).Result;
            return Created($"/api/registration/{entry.Id.ToString()}", entry);
        }
    }
}
