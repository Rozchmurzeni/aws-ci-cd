namespace ExternalScoringService
{
    internal static class ScoreCalculator
    {
        internal static int Calculate(PeselNumber requestPeselNumber) => (requestPeselNumber.FirstPart + requestPeselNumber.SecondPart) % 101;
    }
}
