using System;
using FluentAssertions;
using LoanOfferer.Domain.Exceptions;
using LoanOfferer.Domain.ValueObjects;
using Xunit;

namespace LoanOfferer.Domain.Test.ValueObjects
{
    public class When_creating_loan_amount
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5000)]
        public void It_should_create_correct_loan_amount_for_correct_value(int correctValue)
        {
            // When
            var loanAmount = new LoanAmount(correctValue);

            // Then
            loanAmount.Should().NotBeNull();
            loanAmount.Value.Should().Be(correctValue);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-5000)]
        public void It_should_throw_exception_for_incorrect_value(int incorrectValue)
        {
            // When
            var action = new Func<LoanAmount>(() => new LoanAmount(incorrectValue));

            // Then
            action.Should().Throw<IncorrectLoanAmountValueException>();
        }
    }
}
