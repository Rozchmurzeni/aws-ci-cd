using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;

namespace ExternalScoringService
{
    public class CalculateScoreFromPersonalNumberResult : APIGatewayProxyResponse
    {
        public CalculateScoreFromPersonalNumberResult(int score)
        {
            StatusCode = (int) HttpStatusCode.OK;
            Body = JsonConvert.SerializeObject(new { Score = score });
        }
    }
}
