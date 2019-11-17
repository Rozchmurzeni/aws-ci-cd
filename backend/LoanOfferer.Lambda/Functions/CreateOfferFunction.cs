using System.Threading.Tasks;
using LoanOfferer.CommandHandlers;
using LoanOfferer.Domain.Infrastructure.Factories;
using LoanOfferer.Domain.Infrastructure.Repositories;
using LoanOfferer.Domain.Infrastructure.Services;
using LoanOfferer.Lambda.Models.Requests;
using LoanOfferer.Lambda.Models.Responses;

namespace LoanOfferer.Lambda.Functions
{
    public class CreateOfferFunction
    {
        public async Task<CreateOfferResponse> ExecuteAsync(CreateOfferAPIGatewayRequest apiGatewayRequest)
        {
            var handler = CreateCreateOfferCommandHandler();
            var loanOffer = await handler.Handle(apiGatewayRequest.ToCreateOfferCommand());
            return CreateOfferResponse.Success(loanOffer.Id.Value, loanOffer.MaxLoanAmount.Value);
        }

        private static CreateOfferCommandHandler CreateCreateOfferCommandHandler()
        {
            var externalApiScoringServiceConfig = new EnvironmentVariablesExternalApiScoringServiceConfig();
            var loanOfferFactory = new LoanOfferFactory();
            var loanOfferRepository = new LoanOfferDynamoDbRepository(loanOfferFactory);
            var scoringService = new ExternalApiScoringService(externalApiScoringServiceConfig);
            var service = new CreateOfferCommandHandler(loanOfferFactory, loanOfferRepository, scoringService);

            return service;
        }
    }
}
