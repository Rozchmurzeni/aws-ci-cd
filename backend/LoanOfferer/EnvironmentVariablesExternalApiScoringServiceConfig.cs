using System;
using LoanOfferer.Domain.Infrastructure.Services;

namespace LoanOfferer
{
    public class EnvironmentVariablesExternalApiScoringServiceConfig : IExternalApiScoringServiceConfig
    {
        public string ApiBaseUrl => Environment.GetEnvironmentVariable("ApiBaseUrl");
        public string ApiKey => Environment.GetEnvironmentVariable("ApiKey");
    }
}
