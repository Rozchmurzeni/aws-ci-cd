using System.Threading.Tasks;
using LoanOfferer.Commands;
using LoanOfferer.Domain.Entities;
using LoanOfferer.Domain.Factories;
using LoanOfferer.Domain.Repositories;
using LoanOfferer.Domain.Services;

namespace LoanOfferer.CommandHandlers
{
    public class CreateOfferCommandHandler
    {
        private readonly ILoanOfferFactory _loanOfferFactory;
        private readonly ILoanOfferRepository _loanOfferRepository;
        private readonly IScoringService _scoringService;

        public CreateOfferCommandHandler(ILoanOfferFactory loanOfferFactory, ILoanOfferRepository loanOfferRepository, IScoringService scoringService)
        {
            _loanOfferFactory = loanOfferFactory;
            _loanOfferRepository = loanOfferRepository;
            _scoringService = scoringService;
        }

        public async Task<ILoanOffer> Handle(CreateOfferCommand command)
        {
            var loanOffer = _loanOfferFactory.Create(command.PeselNumber, command.EmailAddress);
            var scoring = await _scoringService.GetScoreAsync(loanOffer.PeselNumber);
            loanOffer.CalculateOffer(scoring);
            await _loanOfferRepository.AddAsync(loanOffer);
            return loanOffer;
        }
    }
}
