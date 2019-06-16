using Amazon.Lambda.Core;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace ExternalScoringService
{
    public class CalculateScoreFromPersonalNumber
    {
        public CalculateScoreFromPersonalNumberResult CalculateScoreFromPersonalNumberLambda(CalculateScoreFromPersonalNumberRequest request)
        {
            var peselNumber = new PeselNumber(request.PeselNumber);
            
            return new CalculateScoreFromPersonalNumberResult(ScoreCalculator.Calculate(peselNumber));
        }
    }
}
