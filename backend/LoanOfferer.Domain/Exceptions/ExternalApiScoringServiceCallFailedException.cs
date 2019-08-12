using System;

namespace LoanOfferer.Domain.Exceptions
{
    public class ExternalApiScoringServiceCallFailedException : Exception
    {
        private const string ExceptionMessage = "Failed to get scoring from external service.";

        public ExternalApiScoringServiceCallFailedException() : base(ExceptionMessage) {}
    }
}
