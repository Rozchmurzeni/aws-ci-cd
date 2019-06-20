using LoanOfferer.Domain.Entities;

namespace LoanOfferer.Domain.Factories
{
    public interface ILoanOfferFactory
    {
        LoanOffer Create(string peselNumber, string emailAddress);
    }
}
