using System.Threading.Tasks;
using LoanOfferer.Domain.Repositories;
using LoanOfferer.Domain.Services;
using LoanOfferer.Domain.ValueObjects;

namespace LoanOfferer.Lambda.Services
{
    public class RequestLoanService
    {
        private readonly ILoanOfferRepository _loanOfferRepository;
        private readonly IEmailNotificationService _emailNotificationService;

        public RequestLoanService(ILoanOfferRepository loanOfferRepository, IEmailNotificationService emailNotificationService)
        {
            _loanOfferRepository = loanOfferRepository;
            _emailNotificationService = emailNotificationService;
        }

        public async Task RequestLoan(string offerId, int requestedAmount)
        {
            var offerEntityIdentity = new EntityIdentity(offerId);
            var requestedLoanAmount = new LoanAmount(requestedAmount);

            var loanOffer = await _loanOfferRepository.GetAsync(offerEntityIdentity);
            loanOffer.SetRequestedLoanAmount(requestedLoanAmount);
            await _loanOfferRepository.UpdateAsync(loanOffer);
            await _emailNotificationService.SendLoanRequestedMessage(loanOffer.EmailAddress, loanOffer.RequestedLoanAmount);
        }
    }
}
