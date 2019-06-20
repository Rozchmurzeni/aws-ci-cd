using Amazon.Lambda.APIGatewayEvents;

namespace ExternalScoringService
{
    public class CalculateScoreFromPersonalNumberRequest : APIGatewayProxyRequest
    {
        private const string QueryStringPeselNumberParameterName = "peselNumber";

        public string PeselNumber
            => QueryStringParameters.ContainsKey(QueryStringPeselNumberParameterName)
                   ? QueryStringParameters[QueryStringPeselNumberParameterName]
                   : null;
    }
}
