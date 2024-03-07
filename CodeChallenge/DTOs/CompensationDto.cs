using CodeChallenge.Models;
using System;

namespace CodeChallenge.DTOs
{
    public class CompensationDto
    {
        public String EmployeeId { get; set; }
        public double Salary { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
