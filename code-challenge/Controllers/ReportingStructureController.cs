using challenge.Models;
using challenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Controllers
{
    [Route("api/reporting")]
    public class ReportingStructureController : Controller
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;

        public ReportingStructureController(ILogger<ReportingStructureController> logger, IEmployeeService service)
        {
            _logger = logger;
            _employeeService = service;
        }

        [HttpGet("{employeeId}", Name = "getEmployeeReportingStructure")]
        public IActionResult GetEmployeeReportingStructure(String employeeId)
        {
            _logger.LogDebug($"Recieved employee reporting structure request for '{employeeId}");

            var employee = _employeeService.GetById(employeeId, true);

            if (employee == null)
                return NotFound();

            // Create reporting structure
            ReportingStructure reportingStructure = new ReportingStructure()
            {
                Employee = employee,
                NumberOfReports = employee.DirectReports.Count + employee.DirectReports.Sum(r => r.DirectReports.Count())
            };

            return Ok(reportingStructure);
        }
    }
}
