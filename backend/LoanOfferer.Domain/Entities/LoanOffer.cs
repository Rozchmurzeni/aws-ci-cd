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
            var maxLoanAmountValue = 1000;
            

            if (score.Value >= 90)
            {
                maxLoanAmountValue = 5000;
            }
            else if (score.Value >= 70)
            {
                maxLoanAmountValue = 3500;
            }
            else if (score.Value >= 50)
            {
                maxLoanAmountValue = 2000;
            }

            MaxLoanAmount = new LoanAmount(maxLoanAmountValue);
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
