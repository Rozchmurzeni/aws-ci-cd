using LoanOfferer.Domain.Entities;

namespace LoanOfferer.Domain.Factories
{
    public interface ILoanOfferFactory
    {
        ILoanOffer Create(string peselNumber, string emailAddress);
        ILoanOffer Create(string offerId, string peselNumber, string emailAddress, int maxLoanAmount);
    }
}
