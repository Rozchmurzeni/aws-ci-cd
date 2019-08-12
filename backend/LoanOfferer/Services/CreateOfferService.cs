using System.Threading.Tasks;
using LoanOfferer.Domain.Entities;
using LoanOfferer.Domain.Factories;
using LoanOfferer.Domain.Repositories;
using LoanOfferer.Domain.Services;

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

        public async Task<ILoanOffer> CreateOfferAsync(string peselNumber, string emailAddress)
        {
            var loanOffer = _loanOfferFactory.Create(peselNumber, emailAddress);
            var scoring = await _scoringService.GetScoreAsync(loanOffer.PeselNumber);
            loanOffer.CalculateOffer(scoring);
            await _loanOfferRepository.AddAsync(loanOffer);
            return loanOffer;
        }
    }
}
