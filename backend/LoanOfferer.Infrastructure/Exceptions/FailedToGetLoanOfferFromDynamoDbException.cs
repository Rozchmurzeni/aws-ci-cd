using System;
using System.Net;

namespace LoanOfferer.Domain.Infrastructure.Exceptions
{
    public class FailedToGetLoanOfferFromDynamoDbException : Exception
    {
        public FailedToGetLoanOfferFromDynamoDbException(HttpStatusCode httpStatusCode) : base($"Failed to get Loan Offer from DynamoDb with code: {httpStatusCode}.") {}
    }
}
