using LoanOfferer.Domain.Entities;
using LoanOfferer.Domain.Repositories;

namespace LoanOfferer.Domain.Infrastructure.Repositories
{
    public class LoanOfferDynamoDbRepository : ILoanOfferRepository
    {
        public void Add(LoanOffer loanOffer) => throw new System.NotImplementedException();
    }
}
