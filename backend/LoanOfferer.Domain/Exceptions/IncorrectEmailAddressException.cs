using System;

namespace LoanOfferer.Domain.Exceptions
{
    public class IncorrectEmailAddressException : Exception
    {
        public IncorrectEmailAddressException(string value) : base($"E-mail address: {value} is incorrect.") {}
    }
}
