using System;

namespace LoanOfferer.Domain.Exceptions
{
    public class IncorrectScoreValueException : Exception
    {
        public IncorrectScoreValueException(int value) : base($"Score value: {value} is incorrect.") {}
    }
}
