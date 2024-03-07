using CodeChallenge.DTOs;
using CodeChallenge.Models;
using CodeChallenge.Repositories;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Helpers
{
    public class CompensationHelper :ICompensationHelper
    {
        private readonly IEmployeeRepository _employeeRepository;
        private ILogger<CompensationHelper> _logger; 

        public CompensationHelper(ILogger<CompensationHelper> logger, IEmployeeRepository employeeRepository) 
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
        }

        public Compensation MapCompensationDto_to_Compensation(CompensationDto dto)
        {
            var employee = _employeeRepository.GetById(dto.EmployeeId);
            if(employee == null) { return null; }

            return new Compensation()
            {
                Employee = employee,
                Salary = dto.Salary,
                EffectiveDate = dto.EffectiveDate,
            };
        }
    }
}
