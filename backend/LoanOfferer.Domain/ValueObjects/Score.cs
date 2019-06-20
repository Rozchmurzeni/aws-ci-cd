using LoanOfferer.Domain.Exceptions;

namespace LoanOfferer.Domain.ValueObjects
{
    public class Score : IValueObject<int>
    {
        private const int MaxScoreValue = 100;
        private const int MinScoreValue = 1;
        
        public Score(int value)
        {
            if (value > MaxScoreValue || value < MinScoreValue)
            {
                throw new IncorrectScoreValueException(value);
            }
            
            Value = value;
        }
        
        public int Value { get; }
    }
}
