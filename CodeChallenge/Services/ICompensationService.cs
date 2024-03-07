using CodeChallenge.DTOs;
using CodeChallenge.Models;
using System;

namespace CodeChallenge.Services
{
    public interface ICompensationService
    {
        Compensation Create(CompensationDto compensation);
        Compensation GetById(String id);
    }
}
