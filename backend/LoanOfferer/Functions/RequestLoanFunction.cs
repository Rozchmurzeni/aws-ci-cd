using System.Threading.Tasks;
using LoanOfferer.Domain.Infrastructure.Factories;
using LoanOfferer.Domain.Infrastructure.Repositories;
using LoanOfferer.Domain.Infrastructure.Services;
using LoanOfferer.Models.Requests;
using LoanOfferer.Models.Responses;
using LoanOfferer.Services;

namespace LoanOfferer.Functions
{
    public class RequestLoanFunction
    {
        public async Task<RequestLoanResponse> ExecuteAsync(RequestLoanRequest request)
        {
            var service = CreateRequestLoanService();
            await service.RequestLoan(request.OfferId, request.RequestedAmount);

            return RequestLoanResponse.Success;
        }

        private static RequestLoanService CreateRequestLoanService()
        {
            var loanOfferFactory = new LoanOfferFactory();
            var loanOfferRepository = new LoanOfferDynamoDbRepository(loanOfferFactory);
            var emailConfig = new EnvironmentVariablesEmailServiceConfig();
            var emailService = new SesEmailNotificationService(emailConfig);
            var service = new RequestLoanService(loanOfferRepository, emailService);

            return service;
        }
    }
}
