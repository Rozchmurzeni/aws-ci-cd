using System;
using LoanOfferer.Domain.Exceptions;

namespace LoanOfferer.Domain.ValueObjects
{
    public class LoanAmount : IValueObject<int>, IComparable<LoanAmount>
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

        public static LoanAmount Zero => new LoanAmount(0);

        public int Value { get; }

        public int CompareTo(LoanAmount other)
        {
            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            if (ReferenceEquals(null, other))
            {
                return 1;
            }

            return Value.CompareTo(other.Value);
        }

        public static bool operator <(LoanAmount firstAmount, LoanAmount secondAmount) => firstAmount.CompareTo(secondAmount) < 0;

        public static bool operator >(LoanAmount firstAmount, LoanAmount secondAmount) => firstAmount.CompareTo(secondAmount) > 0;

        public override string ToString() => Value.ToString();
    }
}
