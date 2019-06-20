using System;
using LoanOfferer.Domain.ValueObjects;

namespace LoanOfferer.Domain.Snapshots
{
    public class LoanOfferSnapshot
    {
        public LoanOfferSnapshot(EntityIdentity id, PeselNumber peselNumber, EmailAddress emailAddress, LoanAmount maxLoanAmount)
        {
            Id = id.Value;
            PeselNumber = peselNumber.Value;
            EmailAddress = emailAddress.Value;
            MaxLoanAmount = maxLoanAmount.Value;
        }

        public Guid Id { get; }
        public string PeselNumber { get; }
        public string EmailAddress { get; }
        public int MaxLoanAmount { get; }
    }
}
