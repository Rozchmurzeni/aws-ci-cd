using System;
using System.Net;

namespace LoanOfferer.Domain.Infrastructure.Exceptions
{
    public class FailedToPutLoanOfferToDynamoDbException : Exception
    {
        public FailedToPutLoanOfferToDynamoDbException(HttpStatusCode httpStatusCode) : base($"Failed to put Loan Offer to DynamoDb with code: {httpStatusCode}.") {}
    }
}
