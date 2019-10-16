using System;
using FluentAssertions;
using LoanOfferer.Domain.Exceptions;
using LoanOfferer.Domain.ValueObjects;
using Xunit;

namespace LoanOfferer.Domain.Test.ValueObjects
{
    public class When_creating_pesel_number
    {
        [Fact]
        public void It_should_create_pesel_number_for_correct_value()
        {
            // Given
            const string correctPeselNumber = "80020372381";

            // When
            var peselNumber = new PeselNumber(correctPeselNumber);

            // Then
            peselNumber.Should().NotBeNull();
            peselNumber.Value.Should().Be(correctPeselNumber);
        }

        [Theory]
        [InlineData("DefinitelyNotPesel")]
        [InlineData("2019")]
        [InlineData("11111111111")]
        public void It_should_throw_exception_for_incorrect_value(string incorrectValue)
        {
            // When
            var action = new Func<PeselNumber>(() => new PeselNumber(incorrectValue));

            // Then
            action.Should().Throw<IncorrectPeselNumberException>();
        }
    }
}
