using System;
using Amazon.Lambda.Core;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace ExternalScoringService
{
    public class CalculateScoreFromPersonalNumber
    {
        public CalculateScoreFromPersonalNumberResponse CalculateScoreFromPersonalNumberLambda(CalculateScoreFromPersonalNumberRequest request)
        {
            if (String.IsNullOrWhiteSpace(request.PeselNumber))
            {
                throw new ArgumentException("Pesel number cannot be null or empty.");
            }

            var peselNumber = new PeselNumber(request.PeselNumber);

            return new CalculateScoreFromPersonalNumberResponse(ScoreCalculator.Calculate(peselNumber));
        }
    }
}
