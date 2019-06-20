using LoanOfferer.Domain.Services;
using LoanOfferer.Domain.Snapshots;
using LoanOfferer.Domain.ValueObjects;

namespace LoanOfferer.Domain.Entities
{
    public class LoanOffer
    {
        private readonly EmailAddress _emailAddress;
        private readonly EntityIdentity _id;
        private readonly PeselNumber _peselNumber;
        private LoanAmount _maxLoanAmount;

        public LoanOffer(EntityIdentity id, PeselNumber peselNumber, EmailAddress emailAddress)
        {
            _id = id;
            _peselNumber = peselNumber;
            _emailAddress = emailAddress;
        }

        public void CalculateOffer(IScoringService scoringService)
        {
            var score = scoringService.GetScore(_peselNumber);
            _maxLoanAmount = CalculateAmount(score);
        }

        public LoanOfferSnapshot GetSnapshot() => new LoanOfferSnapshot(_id, _peselNumber, _emailAddress, _maxLoanAmount);

        private static LoanAmount CalculateAmount(Score score)
        {
            if (score.Value >= 90)
            {
                return new LoanAmount(5000);
            }

            if (score.Value >= 70)
            {
                return new LoanAmount(3500);
            }

            return score.Value >= 50
                       ? new LoanAmount(2000)
                       : new LoanAmount(1000);
        }
    }
}
