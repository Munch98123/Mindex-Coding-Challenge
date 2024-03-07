using CodeChallenge.DTOs;
using CodeChallenge.Models;

namespace CodeChallenge.Helpers
{
    public interface ICompensationHelper
    {
        Compensation MapCompensationDto_to_Compensation(CompensationDto dto);
    }
}
