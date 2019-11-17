using System.Threading.Tasks;
using LoanOfferer.CommandHandlers;
using LoanOfferer.Domain.Infrastructure.Factories;
using LoanOfferer.Domain.Infrastructure.Repositories;
using LoanOfferer.Domain.Infrastructure.Services;
using LoanOfferer.Lambda.Models.Requests;
using LoanOfferer.Lambda.Models.Responses;

namespace LoanOfferer.Lambda.Functions
{
    public class RequestLoanFunction
    {
        public async Task<RequestLoanResponse> ExecuteAsync(RequestLoanAPIGatewayRequest request)
        {
            var handler = CreateRequestLoanCommandHandler();
            await handler.Handle(request.ToRequestLoanCommand());

            return RequestLoanResponse.Success;
        }

        private static RequestLoanCommandHandler CreateRequestLoanCommandHandler()
        {
            var loanOfferFactory = new LoanOfferFactory();
            var loanOfferRepository = new LoanOfferDynamoDbRepository(loanOfferFactory);
            var emailConfig = new EnvironmentVariablesEmailServiceConfig();
            var emailService = new SesEmailNotificationService(emailConfig);
            var service = new RequestLoanCommandHandler(loanOfferRepository, emailService);

            return service;
        }
    }
}
