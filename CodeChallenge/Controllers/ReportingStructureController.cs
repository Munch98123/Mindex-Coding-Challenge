using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/reportingStructure")]
    public class ReportingStructureController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IReportingStructureService _reportingStructureService;

        public ReportingStructureController(ILogger<ReportingStructureController> logger, IReportingStructureService reportingStructureService)
        {
            _logger = logger;
            _reportingStructureService = reportingStructureService;
        }

        [HttpGet("{id}", Name = "getReportingStructure")]
        public IActionResult GetReportingStructure(String id) 
        {
            _logger.LogDebug($"Recieved reporting structure request for '{id}'");

            var reportingStructure = _reportingStructureService.GetReportingStructure(id);

            return reportingStructure != null ? Ok(reportingStructure) : NotFound();
        }
    }
}
