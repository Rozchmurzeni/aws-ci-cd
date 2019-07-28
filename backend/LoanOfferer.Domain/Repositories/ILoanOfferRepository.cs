using System.Threading.Tasks;
using LoanOfferer.Domain.Entities;
using LoanOfferer.Domain.ValueObjects;

namespace LoanOfferer.Domain.Repositories
{
    public interface ILoanOfferRepository
    {
        Task AddAsync(ILoanOffer loanOffer);
        Task<ILoanOffer> GetAsync(EntityIdentity offerId);
        Task UpdateAsync(ILoanOffer loanOffer);
    }
}
