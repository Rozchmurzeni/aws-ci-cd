using System;

namespace LoanOfferer.Domain.Exceptions
{
    public class ExternalApiScoringServiceCallFailedException : Exception
    {
        public ExternalApiScoringServiceCallFailedException() : base(ExceptionMessage) {}

        private const string ExceptionMessage = "Failed to get scoring from external service.";
    }
}
