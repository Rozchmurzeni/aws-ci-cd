using LoanOfferer.Domain.Services;
using LoanOfferer.Domain.ValueObjects;

namespace LoanOfferer.Domain.Infrastructure.Services
{
    public class ExternalApiScoringService : IScoringService
    {
        public Score GetScore(PeselNumber peselNumber) => throw new System.NotImplementedException();
    }
}
