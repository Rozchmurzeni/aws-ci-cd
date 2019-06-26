using LoanOfferer.Domain.Entities;

namespace LoanOfferer.Domain.Factories
{
    public interface ILoanOfferFactory
    {
        LoanOffer Create(string peselNumber, string emailAddress);
        LoanOffer Create(string offerId, string peselNumber, string emailAddress, int maxLoanAmount);
    }
}
