using CodeChallenge.DTOs;
using CodeChallenge.Models;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/compensation")]
    public class CompensationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;

        public CompensationController(ILogger logger, ICompensationService compensationService)
        {
            _logger = logger;
            _compensationService = compensationService;
        }

        [HttpPost]
        public IActionResult CreateCompensation([FromBody] CompensationDto compensationDto)
        {
            _logger.LogDebug($"Received compensation create request for '{compensationDto.EmployeeId}''");

            Compensation compensationDtoRequest = _compensationService.Create(compensationDto);

            return compensationDtoRequest != null ? CreatedAtRoute("getCompensationById", new { id = compensationDtoRequest.Employee.EmployeeId }, compensationDtoRequest) : NotFound();
        }

        [HttpGet("{id}", Name = "getCompentsationById")]
        public IActionResult GetCompensationById(String id)
        {
            _logger.LogDebug($"Received comepnsation get request for '{id}'");

            var compensation = _compensationService.GetById(id);

            return compensation != null ? Ok(compensation) : NotFound();
        }
    }
}
