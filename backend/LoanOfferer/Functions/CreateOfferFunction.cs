using LoanOfferer.Domain.Infrastructure.Factories;
using LoanOfferer.Domain.Infrastructure.Repositories;
using LoanOfferer.Domain.Infrastructure.Services;
using LoanOfferer.Models.Requests;
using LoanOfferer.Models.Responses;
using LoanOfferer.Services;

namespace LoanOfferer.Functions
{
    public class CreateOfferFunction
    {
        public CreateOfferResponse Execute(CreateOfferRequest request)
        {
            var service = CreateOfferService();
            var loanOffer = service.CreateOffer(request.PeselNumber, request.EmailAddress);
            return new CreateOfferResponse(loanOffer.Id, loanOffer.MaxLoanAmount);
        }

        private static CreateOfferService CreateOfferService()
        {
            var externalApiScoringServiceConfig = new EnvironmentVariablesExternalApiScoringServiceConfig();
            var loanOfferRepository = new LoanOfferDynamoDbRepository();
            var loanOfferFactory = new LoanOfferFactory();
            var scoringService = new ExternalApiScoringService(externalApiScoringServiceConfig);
            var service = new CreateOfferService(loanOfferFactory, loanOfferRepository, scoringService);
            
            return service;
        }
    }
}
