using LoanOfferer.Domain.Factories;
using LoanOfferer.Domain.Repositories;
using LoanOfferer.Domain.Services;
using LoanOfferer.Domain.Snapshots;

namespace LoanOfferer.Services
{
    public class CreateOfferService
    {
        private readonly ILoanOfferFactory _loanOfferFactory;
        private readonly ILoanOfferRepository _loanOfferRepository;
        private readonly IScoringService _scoringService;
        
        public CreateOfferService(ILoanOfferFactory loanOfferFactory, ILoanOfferRepository loanOfferRepository, IScoringService scoringService)
        {
            _loanOfferFactory = loanOfferFactory;
            _loanOfferRepository = loanOfferRepository;
            _scoringService = scoringService;
        }

        public LoanOfferSnapshot CreateOffer(string peselNumber, string emailAddress)
        {
            var loanOffer = _loanOfferFactory.Create(peselNumber, emailAddress);
            loanOffer.CalculateOffer(_scoringService);
            _loanOfferRepository.Add(loanOffer);
            return loanOffer.GetSnapshot();
        }
    }
}
