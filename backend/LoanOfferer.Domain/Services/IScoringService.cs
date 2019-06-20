using LoanOfferer.Domain.ValueObjects;

namespace LoanOfferer.Domain.Services
{
    public interface IScoringService
    {
        Score GetScore(PeselNumber peselNumber);
    }
}
