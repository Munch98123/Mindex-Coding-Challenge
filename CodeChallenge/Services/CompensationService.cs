using CodeChallenge.DTOs;
using CodeChallenge.Helpers;
using CodeChallenge.Models;
using CodeChallenge.Repositories;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly ILogger<CompensationService> _logger;
        private readonly ICompensationHelper _helper;

        public CompensationService(ILogger<CompensationService> logger, ICompensationRepository compensationRepository, ICompensationHelper helper)
        {
            _logger = logger;
            _compensationRepository = compensationRepository;
            _helper = helper;
        }

        public Compensation Create(CompensationDto compensation)
        {
            if (compensation == null) { return null; }

            var employeeCompensation = _helper.MapCompensationDto_to_Compensation(compensation);

            if(employeeCompensation == null) { return null; }

            var newEmployeeCompensation = _compensationRepository.Add(employeeCompensation);

            _compensationRepository.SaveAsync().Wait();

            return newEmployeeCompensation;

        }

        public Compensation GetById(string id)
        {
            return id != null ? _compensationRepository.GetById(id) : null;
        }
    }
}
