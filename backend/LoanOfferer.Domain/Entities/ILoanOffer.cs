using LoanOfferer.Domain.Services;
using LoanOfferer.Domain.ValueObjects;

namespace LoanOfferer.Domain.Entities
{
    public interface ILoanOffer
    {
        EntityIdentity Id { get; }
        PeselNumber PeselNumber { get; }
        EmailAddress EmailAddress { get; }
        LoanAmount MaxLoanAmount { get; }
        LoanAmount RequestedLoanAmount { get; }
        void CalculateOffer(Score score);
        void SetRequestedLoanAmount(LoanAmount requestedLoanAmount);
    }
}
