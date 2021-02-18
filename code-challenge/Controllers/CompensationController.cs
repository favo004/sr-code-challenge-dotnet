using challenge.Models;
using challenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace challenge.Controllers
{
    [Route("api/compensation")]
    public class CompensationController : Controller
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;

        public CompensationController(ILogger<CompensationController> logger, ICompensationService service)
        {
            _logger = logger;
            _compensationService = service;
        }

        [HttpPost]
        public IActionResult AddCompensation([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Adding compensation with employee id '{compensation.EmployeeId}' to DB");

            _compensationService.Create(compensation);

            return CreatedAtRoute("getCompensationByEmployeeId", new { employeeId = compensation.EmployeeId }, compensation);
        }

        [HttpGet("{employeeId}", Name = "getCompensationByEmployeeId")]
        public IActionResult GetCompensationByEmployeeId(String employeeId)
        {
            _logger.LogDebug($"Retrieving compensation by employee id '{employeeId}'");

            var compensation = _compensationService.GetByEmployeeId(employeeId);

            if (compensation == null)
                return NotFound();

            return Ok(compensation);
        }
    }
}
