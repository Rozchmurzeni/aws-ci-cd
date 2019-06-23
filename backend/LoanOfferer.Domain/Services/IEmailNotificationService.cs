using System.Threading.Tasks;
using LoanOfferer.Domain.ValueObjects;

namespace LoanOfferer.Domain.Services
{
    public interface IEmailNotificationService
    {
        Task SendLoanRequestedMessage(EmailAddress emailAddress, LoanAmount requestedLoanAmount);
    }
}
