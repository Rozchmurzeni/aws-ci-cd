using LoanOfferer.Domain.Exceptions;

namespace LoanOfferer.Domain.ValueObjects
{
    public class LoanAmount : IValueObject<int>
    {
        private const int MinLoanAmountValue = 0;
        
        public LoanAmount(int value)
        {
            if (value < MinLoanAmountValue)
            {
                throw new IncorrectLoanAmountValueException(value);
            }

            Value = value;
        }

        public int Value { get; }
    }
}
