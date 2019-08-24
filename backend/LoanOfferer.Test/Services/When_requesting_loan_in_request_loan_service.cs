using System.Threading.Tasks;
using LoanOfferer.Domain.Entities;
using LoanOfferer.Domain.Repositories;
using LoanOfferer.Domain.Services;
using LoanOfferer.Domain.ValueObjects;
using LoanOfferer.Services;
using Moq;
using Xunit;

namespace LoanOfferer.Test.Services
{
    public class When_requesting_loan_in_request_loan_service
    {
        [Fact]
        public async Task It_should_get_loan_offer_from_repository_using_offer_id()
        {
            // Given
            const int requestedAmount = 5000;
            var offerEntityIdentity = EntityIdentity.New;
            var repositoryMock = CreateLoanOfferRepositoryMock(offerEntityIdentity);
            var service = CreateRequestLoanService(repositoryMock);

            // When
            await service.RequestLoan(offerEntityIdentity.ToString(), requestedAmount);

            // Then
            repositoryMock.Verify(m => m.GetAsync(It.Is<EntityIdentity>(id => id.Value == offerEntityIdentity.Value)), Times.Once);
        }

        [Fact]
        public async Task It_should_call_set_requested_loan_amount_with_given_loan_amount()
        {
            // Given
            const int requestedAmountPrimitive = 5000;
            var requestedLoanAmount = new LoanAmount(requestedAmountPrimitive);
            var offerEntityIdentity = EntityIdentity.New;
            var loanOffer = new Mock<ILoanOffer>();
            var repositoryMock = CreateLoanOfferRepositoryMock(offerEntityIdentity, loanOffer);
            var service = CreateRequestLoanService(repositoryMock);

            // When
            await service.RequestLoan(offerEntityIdentity.ToString(), requestedLoanAmount.Value);

            // Then
            loanOffer.Verify(m => m.SetRequestedLoanAmount(It.Is<LoanAmount>(amount => amount.Value == requestedLoanAmount.Value)), Times.Once);
        }

        [Fact]
        public async Task It_should_call_update_in_repository_using_the_same_loan_offer()
        {
            // Given
            const int requestedAmountPrimitive = 5000;
            var requestedLoanAmount = new LoanAmount(requestedAmountPrimitive);
            var offerEntityIdentity = EntityIdentity.New;
            var loanOffer = new Mock<ILoanOffer>();
            var repositoryMock = CreateLoanOfferRepositoryMock(offerEntityIdentity, loanOffer);
            var service = CreateRequestLoanService(repositoryMock);

            // When
            await service.RequestLoan(offerEntityIdentity.ToString(), requestedLoanAmount.Value);

            // Then
            repositoryMock.Verify(m => m.UpdateAsync(loanOffer.Object), Times.Never);
        }

        [Fact]
        public async Task It_should_call_email_notification_service_with_parameters_from_loan_offer()
        {
            // Given
            const int requestedAmountPrimitive = 5000;
            var requestedLoanAmount = new LoanAmount(requestedAmountPrimitive);
            var offerEntityIdentity = EntityIdentity.New;
            var loanOffer = new Mock<ILoanOffer>();
            var repositoryMock = CreateLoanOfferRepositoryMock(offerEntityIdentity, loanOffer);
            var emailNotificationServiceMock = new Mock<IEmailNotificationService>();
            var service = CreateRequestLoanService(repositoryMock, emailNotificationServiceMock);

            // When
            await service.RequestLoan(offerEntityIdentity.ToString(), requestedLoanAmount.Value);

            // Then
            emailNotificationServiceMock.Verify(m => m.SendLoanRequestedMessage(loanOffer.Object.EmailAddress, loanOffer.Object.RequestedLoanAmount), Times.Once);
        }

        private static Mock<ILoanOfferRepository> CreateLoanOfferRepositoryMock(EntityIdentity offerEntityIdentity, IMock<ILoanOffer> loanOfferMock = null)
        {
            var mock = new Mock<ILoanOfferRepository>();
            mock.Setup(m => m.GetAsync(It.Is<EntityIdentity>(id => id.Value == offerEntityIdentity.Value)))
                .ReturnsAsync(loanOfferMock?.Object ?? new Mock<ILoanOffer>().Object);
            return mock;
        }

        private static RequestLoanService CreateRequestLoanService(
            IMock<ILoanOfferRepository> loanOfferRepositoryMock = null,
            IMock<IEmailNotificationService> emailNotificationService = null
        )
            => new RequestLoanService(
                loanOfferRepositoryMock?.Object ?? new Mock<ILoanOfferRepository>().Object,
                emailNotificationService?.Object ?? new Mock<IEmailNotificationService>().Object
            );
    }
}
