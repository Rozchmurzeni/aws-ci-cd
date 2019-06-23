using System;
using System.ComponentModel.DataAnnotations;
using LoanOfferer.Domain.Exceptions;

namespace LoanOfferer.Domain.ValueObjects
{
    public class EmailAddress : IValueObject<string>
    {
        public EmailAddress(string value)
        {
            if (!IsEmailValid(value))
            {
                throw new IncorrectEmailAddressException(value);
            }

            Value = value;
        }

        public string Value { get; }

        private static bool IsEmailValid(string value)
            => !String.IsNullOrWhiteSpace(value)
            && new EmailAddressAttribute().IsValid(value);
    }
}
