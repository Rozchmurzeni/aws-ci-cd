using LoanOfferer.Domain.Entities;
using LoanOfferer.Domain.ValueObjects;

namespace LoanOfferer.Domain.Test.TestHelpers
{
    public class LoanOfferBuilder
    {
        private readonly EntityIdentity _id = EntityIdentity.New;
        private readonly EmailAddress _emailAddress = new EmailAddress("example@example.com");
        private readonly PeselNumber _peselNumber = new PeselNumber("81021799999");
        private LoanAmount _maxLoanAmount;

        public LoanOffer Build()
            => _maxLoanAmount is null
                   ? new LoanOffer(_id, _peselNumber, _emailAddress)
                   : new LoanOffer(_id, _peselNumber, _emailAddress, _maxLoanAmount);

        public LoanOfferBuilder WithMaxLoanAmount(LoanAmount maxLoanAmount)
        {
            _maxLoanAmount = maxLoanAmount;
            return this;
        }
    }
}
