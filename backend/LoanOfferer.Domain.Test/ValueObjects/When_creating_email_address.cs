using System;
using FluentAssertions;
using FluentAssertions.Common;
using LoanOfferer.Domain.Exceptions;
using LoanOfferer.Domain.ValueObjects;
using Xunit;

namespace LoanOfferer.Domain.Test.ValueObjects
{
    public class When_creating_email_address
    {
        [Fact]
        public void It_should_create_correct_email_for_correct_value()
        {
            // Given
            const string correctEmailAddress = "example@gmail.com";

            // When
            var email = new EmailAddress(correctEmailAddress);

            // Then
            email.Should().NotBeNull();
            email.Value.IsSameOrEqualTo(correctEmailAddress);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData("\t\t\t\t\t\t\t\t\n\n\n\n")]
        public void It_should_throw_exception_for_null_or_whitespace_value(string incorrectValue)
        {
            // When
            var action = new Func<EmailAddress>(() => new EmailAddress(incorrectValue));

            // Then
            action.Should().Throw<IncorrectEmailAddressException>();
        }

        [Theory]
        [InlineData("plainAddress")]
        [InlineData("@%^%#$@#$@#.com")]
        [InlineData("@domain.com")]
        [InlineData("email.domain.com")]
        [InlineData("email@domain@domain.com")]
        public void It_should_throw_exception_for_incorrect_email_format_value(string incorrectValue)
        {
            // When
            var action = new Func<EmailAddress>(() => new EmailAddress(incorrectValue));

            // Then
            action.Should().Throw<IncorrectEmailAddressException>();
        }
    }
}
