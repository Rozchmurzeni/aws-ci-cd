using System.Threading.Tasks;
using Amazon.Lambda.Core;
using LoanOfferer.Domain.Infrastructure.Factories;
using LoanOfferer.Domain.Infrastructure.Repositories;
using LoanOfferer.Domain.Infrastructure.Services;
using LoanOfferer.Lambda.Models.Requests;
using LoanOfferer.Lambda.Models.Responses;
using LoanOfferer.Lambda.Services;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace LoanOfferer.Lambda.Functions
{
    public class CreateOfferFunction
    {
        public async Task<CreateOfferResponse> ExecuteAsync(CreateOfferRequest request)
        {
            var service = CreateOfferService();
            var loanOffer = await service.CreateOfferAsync(request.PeselNumber, request.EmailAddress);
            return CreateOfferResponse.Success(loanOffer.Id.Value, loanOffer.MaxLoanAmount.Value);
        }

        private static CreateOfferService CreateOfferService()
        {
            var externalApiScoringServiceConfig = new EnvironmentVariablesExternalApiScoringServiceConfig();
            var loanOfferFactory = new LoanOfferFactory();
            var loanOfferRepository = new LoanOfferDynamoDbRepository(loanOfferFactory);
            var scoringService = new ExternalApiScoringService(externalApiScoringServiceConfig);
            var service = new CreateOfferService(loanOfferFactory, loanOfferRepository, scoringService);
            
            return service;
        }
    }
}
