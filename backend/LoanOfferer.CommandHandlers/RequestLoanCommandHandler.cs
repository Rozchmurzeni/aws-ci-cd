using System.Threading.Tasks;
using LoanOfferer.Commands;
using LoanOfferer.Domain.Repositories;
using LoanOfferer.Domain.Services;
using LoanOfferer.Domain.ValueObjects;

namespace LoanOfferer.CommandHandlers
{
    public class RequestLoanCommandHandler
    {
        private readonly ILoanOfferRepository _loanOfferRepository;
        private readonly IEmailNotificationService _emailNotificationService;

        public RequestLoanCommandHandler(ILoanOfferRepository loanOfferRepository, IEmailNotificationService emailNotificationService)
        {
            _loanOfferRepository = loanOfferRepository;
            _emailNotificationService = emailNotificationService;
        }

        public async Task Handle(RequestLoanCommand command)
        {
            var offerEntityIdentity = new EntityIdentity(command.OfferId);
            var requestedLoanAmount = new LoanAmount(command.RequestedAmount);

            var loanOffer = await _loanOfferRepository.GetAsync(offerEntityIdentity);
            loanOffer.SetRequestedLoanAmount(requestedLoanAmount);
            await _loanOfferRepository.UpdateAsync(loanOffer);
            await _emailNotificationService.SendLoanRequestedMessage(loanOffer.EmailAddress, loanOffer.RequestedLoanAmount);
        }
    }
}
