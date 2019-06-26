using System;
using FluentAssertions;
using FluentAssertions.Common;
using LoanOfferer.Domain.Exceptions;
using LoanOfferer.Domain.ValueObjects;
using Xunit;

namespace LoanOfferer.Domain.Test.ValueObjects
{
    public class When_creating_score
    {
        [Theory]
        [InlineData(1)]
        [InlineData(50)]
        [InlineData(100)]
        public void It_should_create_correct_score_for_correct_value(int correctValue)
        {
            // When
            var score = new Score(correctValue);

            // Then
            score.Should().NotBeNull();
            score.Value.IsSameOrEqualTo(correctValue);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(101)]
        [InlineData(200)]
        public void It_should_throw_exception_for_incorrect_value(int incorrectValue)
        {
            // When
            var action = new Func<Score>(() => new Score(incorrectValue));

            // Then
            action.Should().Throw<IncorrectScoreValueException>();
        }
    }
}
