using System.Threading.Tasks;
using LoanOfferer.Domain.ValueObjects;

namespace LoanOfferer.Domain.Services
{
    public interface IScoringService
    {
        Task<Score> GetScoreAsync(PeselNumber peselNumber);
    }
}
