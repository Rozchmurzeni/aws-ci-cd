using LoanOfferer.Domain.Entities;
using LoanOfferer.Domain.Factories;
using LoanOfferer.Domain.ValueObjects;

namespace LoanOfferer.Domain.Infrastructure.Factories
{
    public class LoanOfferFactory : ILoanOfferFactory
    {
        public ILoanOffer Create(string peselNumber, string emailAddress) => new LoanOffer(EntityIdentity.New, new PeselNumber(peselNumber), new EmailAddress(emailAddress));

        public ILoanOffer Create(string offerId, string peselNumber, string emailAddress, int maxLoanAmount)
            => new LoanOffer(new EntityIdentity(offerId), new PeselNumber(peselNumber), new EmailAddress(emailAddress), new LoanAmount(maxLoanAmount));
    }
}
