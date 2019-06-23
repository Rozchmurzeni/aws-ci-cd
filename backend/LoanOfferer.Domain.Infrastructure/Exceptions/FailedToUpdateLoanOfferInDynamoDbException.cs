using System;
using System.Net;

namespace LoanOfferer.Domain.Infrastructure.Exceptions
{
    public class FailedToUpdateLoanOfferInDynamoDbException : Exception
    {
        public FailedToUpdateLoanOfferInDynamoDbException(HttpStatusCode httpStatusCode) : base($"Failed to update Loan Offer in DynamoDb with code: {httpStatusCode}.") {}
    }
}
