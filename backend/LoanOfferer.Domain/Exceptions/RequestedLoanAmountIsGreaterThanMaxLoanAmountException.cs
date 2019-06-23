using System;
using LoanOfferer.Domain.ValueObjects;

namespace LoanOfferer.Domain.Exceptions
{
    public class RequestedLoanAmountIsGreaterThanMaxLoanAmountException : Exception
    {
        public RequestedLoanAmountIsGreaterThanMaxLoanAmountException(EntityIdentity id)
            : base($"Requested loan amount is greater than max loan amount for loan with ID: {id}.") {}
    }
}
