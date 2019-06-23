using System.Threading.Tasks;
using LoanOfferer.Domain.Entities;
using LoanOfferer.Domain.ValueObjects;

namespace LoanOfferer.Domain.Repositories
{
    public interface ILoanOfferRepository
    {
        Task AddAsync(LoanOffer loanOffer);
        Task<LoanOffer> GetAsync(EntityIdentity offerId);
        Task UpdateAsync(LoanOffer loanOffer);
    }
}
