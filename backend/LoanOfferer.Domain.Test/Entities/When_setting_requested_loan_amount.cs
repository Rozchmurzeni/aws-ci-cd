using System;
using FluentAssertions;
using FluentAssertions.Common;
using LoanOfferer.Domain.Exceptions;
using LoanOfferer.Domain.Test.TestHelpers;
using LoanOfferer.Domain.ValueObjects;
using Xunit;

namespace LoanOfferer.Domain.Test.Entities
{
    public class When_setting_requested_loan_amount
    {
        private const int MaxLoanAmountValue = 1000;

        [Fact]
        public void It_should_correctly_set_for_not_greater_value_than_max_amount()
        {
            // Given
            var maxLoanAmount = new LoanAmount(MaxLoanAmountValue);
            var requestedLoanAmount = new LoanAmount(MaxLoanAmountValue - 100);
            var loanOffer = new LoanOfferBuilder().WithMaxLoanAmount(maxLoanAmount).Build();

            // When
            loanOffer.SetRequestedLoanAmount(requestedLoanAmount);

            // Then
            loanOffer.RequestedLoanAmount.Should().NotBeNull();
            loanOffer.RequestedLoanAmount.Value.IsSameOrEqualTo(requestedLoanAmount.Value);
        }

        [Fact]
        public void It_should_throw_exception_for_greater_value_than_max_amount()
        {
            // Given
            var maxLoanAmount = new LoanAmount(MaxLoanAmountValue);
            var requestedLoanAmount = new LoanAmount(MaxLoanAmountValue + 100);
            var loanOffer = new LoanOfferBuilder().WithMaxLoanAmount(maxLoanAmount).Build();

            // When
            var action = new Action(() => loanOffer.SetRequestedLoanAmount(requestedLoanAmount));

            // Then
            action.Should().Throw<RequestedLoanAmountIsGreaterThanMaxLoanAmountException>();
        }
    }
}
