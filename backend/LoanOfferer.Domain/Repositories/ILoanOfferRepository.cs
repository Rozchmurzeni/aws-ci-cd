using LoanOfferer.Domain.Entities;

namespace LoanOfferer.Domain.Repositories
{
    public interface ILoanOfferRepository
    {
        void Add(LoanOffer loanOffer);
    }
}
