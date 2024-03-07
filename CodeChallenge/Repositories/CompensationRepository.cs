using CodeChallenge.Data;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<ICompensationRepository> _logger;

        public CompensationRepository(EmployeeContext compensationContext, ILogger<ICompensationRepository> logger)
        {
            _employeeContext = compensationContext;
            _logger = logger;
        }

        public Compensation Add(Compensation compensation)
        {
            compensation.CompensationId = Guid.NewGuid().ToString();
            _employeeContext.Compensation.Add(compensation);
            return compensation;
        }

        public Compensation GetById(string id)
        {
            return _employeeContext.Compensation.SingleOrDefault(c => c.CompensationId == id);
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        public Compensation Remove(Compensation compensation)
        {
            return _employeeContext.Remove(compensation).Entity;
        }


    }
}
