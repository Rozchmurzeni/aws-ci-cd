using FluentAssertions;
using LoanOfferer.Domain.ValueObjects;
using Xunit;

namespace LoanOfferer.Domain.Test.ValueObjects
{
    public class When_comparing_loan_amounts
    {
        [Fact]
        public void It_should_correctly_point_greater_loan_amount()
        {
            // Given
            const int greaterValue = 500;
            const int smallerValue = 400;

            // When
            var greaterLoanAmount = new LoanAmount(greaterValue);
            var smallerLoanAmount = new LoanAmount(smallerValue);
            var isGreaterLoanAmountGreater = greaterLoanAmount > smallerLoanAmount;

            // Then
            isGreaterLoanAmountGreater.Should().BeTrue();
        }

        [Fact]
        public void It_should_correctly_point_smaller_loan_amount()
        {
            // Given
            const int greaterValue = 500;
            const int smallerValue = 400;

            // When
            var greaterLoanAmount = new LoanAmount(greaterValue);
            var smallerLoanAmount = new LoanAmount(smallerValue);
            var isSmallerLoanAmountSmaller = smallerLoanAmount < greaterLoanAmount;

            // Then
            isSmallerLoanAmountSmaller.Should().BeTrue();
        }
    }
}
