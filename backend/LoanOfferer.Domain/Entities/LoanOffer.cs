using LoanOfferer.Domain.Exceptions;
using LoanOfferer.Domain.ValueObjects;

namespace LoanOfferer.Domain.Entities
{
    public class LoanOffer : ILoanOffer
    {
        private LoanAmount _maxLoanAmount;
        private LoanAmount _requestedLoanAmount;

        public LoanOffer(EntityIdentity id, PeselNumber peselNumber, EmailAddress emailAddress)
        {
            Id = id;
            PeselNumber = peselNumber;
            EmailAddress = emailAddress;
        }

        public LoanOffer(EntityIdentity id, PeselNumber peselNumber, EmailAddress emailAddress, LoanAmount maxLoanAmount) : this(id, peselNumber, emailAddress)
        {
            MaxLoanAmount = maxLoanAmount;
        }

        public EntityIdentity Id { get; }
        public PeselNumber PeselNumber { get; }
        public EmailAddress EmailAddress { get; }

        public LoanAmount MaxLoanAmount
        {
            get => _maxLoanAmount ?? LoanAmount.Zero;
            private set => _maxLoanAmount = value;
        }

        public LoanAmount RequestedLoanAmount
        {
            get => _requestedLoanAmount ?? LoanAmount.Zero;
            private set => _requestedLoanAmount = value;
        }

        public void CalculateOffer(Score score)
        {
            if (score.Value >= 90)
            {
                MaxLoanAmount = new LoanAmount(5000);
            }

            if (score.Value >= 70)
            {
                MaxLoanAmount = new LoanAmount(3500);
            }

            MaxLoanAmount = score.Value >= 50
                                ? new LoanAmount(2000)
                                : new LoanAmount(1000);
        }

        public void SetRequestedLoanAmount(LoanAmount requestedLoanAmount)
        {
            if (requestedLoanAmount > MaxLoanAmount)
            {
                throw new RequestedLoanAmountIsGreaterThanMaxLoanAmountException(Id);
            }

            RequestedLoanAmount = requestedLoanAmount;
        }
    }
}
