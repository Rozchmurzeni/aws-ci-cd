namespace ExternalScoringService
{
    public class CalculateScoreFromPersonalNumberResult
    {
        public CalculateScoreFromPersonalNumberResult(int score)
        {
            Score = score;
        }

        public int Score { get; }
    }
}
