using CodeChallenge.Models;
using CodeChallenge.Repositories;
using Microsoft.Extensions.Logging;
using System;

namespace CodeChallenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<ReportingStructureService> _logger;

        public ReportingStructureService(ILogger<ReportingStructureService> logger, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }
        #region Main Methods
        public ReportingStructure GetReportingStructure(String id)
        {
            var employee = _employeeRepository.GetById(id);

            if(employee == null) { return null; }

            var totalReports = GetTotalReports(employee);

            return new ReportingStructure()
            {
                Employee = employee,
                NumberOfReports = totalReports
            };
        }
        #endregion

        #region Helper Methods
        public int GetTotalReports(Employee employee)
        {

            //add current employee direct reports
            int totalReports = employee.DirectReports.Count;
            foreach (var report in employee.DirectReports)
            {
                //get first employee in direct reports and recursively call this function till end of list
                var currEmployee = _employeeRepository.GetById(report.EmployeeId);
                totalReports += GetTotalReports(currEmployee);
            }
            return totalReports;
        }
        #endregion
    }
}
