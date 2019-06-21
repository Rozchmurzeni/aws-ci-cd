using LoanOfferer.Domain.Infrastructure.Services.Models;
using LoanOfferer.Domain.Services;
using LoanOfferer.Domain.ValueObjects;
using RestSharp;

namespace LoanOfferer.Domain.Infrastructure.Services
{
    public class ExternalApiScoringService : IScoringService
    {
        private readonly IExternalApiScoringServiceConfig _serviceConfig;
        
        public ExternalApiScoringService(IExternalApiScoringServiceConfig serviceConfig)
        {
            _serviceConfig = serviceConfig;
        }

        public Score GetScore(PeselNumber peselNumber)
        {
            var restClient = new RestClient(_serviceConfig.ApiBaseUrl);
            var request = new RestRequest("scoring", Method.GET);
            request.AddQueryParameter("peselNumber", peselNumber.Value);
            request.AddHeader("x-api-key", _serviceConfig.ApiKey);
            
            var response = restClient.Execute<GetScoreResponse>(request);
            
            return new Score(response.Data.Score);
        }
    }
}
