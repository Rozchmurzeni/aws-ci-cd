using System;
using System.Net;

namespace LoanOfferer.Domain.Infrastructure.Exceptions
{
    public class FailedToPutLoanOfferToDynamoDb : Exception
    {
        public FailedToPutLoanOfferToDynamoDb(HttpStatusCode httpStatusCode) : base($"Failed to put Loan Offer to DynamoDb with code: {httpStatusCode}") {}
    }
}
