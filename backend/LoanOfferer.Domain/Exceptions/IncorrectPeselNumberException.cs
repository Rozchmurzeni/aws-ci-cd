using System;

namespace LoanOfferer.Domain.Exceptions
{
    public class IncorrectPeselNumberException : Exception
    {
        public IncorrectPeselNumberException(string value) : base($"PESEL number: {value} is incorrect.") {}
    }
}
