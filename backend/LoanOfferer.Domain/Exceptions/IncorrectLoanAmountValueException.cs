using System;

namespace LoanOfferer.Domain.Exceptions
{
    public class IncorrectLoanAmountValueException : Exception
    {
        public IncorrectLoanAmountValueException(int value) : base($"Loan amount value: {value} is incorrect.") {}
    }
}
