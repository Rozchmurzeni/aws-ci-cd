namespace LoanOfferer.Domain.Infrastructure.Services
{
    public interface IExternalApiScoringServiceConfig
    {
        string ApiBaseUrl { get; }
        string ApiKey { get; }
    }
}
