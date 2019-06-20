using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;

namespace ExternalScoringService
{
    public class CalculateScoreFromPersonalNumberResponse : APIGatewayProxyResponse
    {
        public CalculateScoreFromPersonalNumberResponse(int score)
        {
            StatusCode = (int) HttpStatusCode.OK;
            Body = JsonConvert.SerializeObject(new { Score = score });
        }
    }
}
