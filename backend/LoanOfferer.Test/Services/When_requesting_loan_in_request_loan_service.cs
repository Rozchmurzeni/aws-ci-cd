using System.Threading.Tasks;
using LoanOfferer.CommandHandlers;
using LoanOfferer.Commands;
using LoanOfferer.Domain.Entities;
using LoanOfferer.Domain.Repositories;
using LoanOfferer.Domain.Services;
using LoanOfferer.Domain.ValueObjects;
using Moq;
using Xunit;

namespace LoanOfferer.Test.Services
{
    public class When_requesting_loan_in_request_loan_service
    {
        private const int TestRequestedAmount = 5000;
        private readonly EntityIdentity _testOfferEntityIdentity = EntityIdentity.New;

        [Fact]
        public async Task It_should_get_loan_offer_from_repository_using_offer_id()
        {
            // Given
            var repositoryMock = CreateLoanOfferRepositoryMock(_testOfferEntityIdentity);
            var command = CreateRequestLoanCommand();
            var handler = CreateRequestLoanCommandHandler(repositoryMock);

            // When
            await handler.Handle(command);

            // Then
            repositoryMock.Verify(m => m.GetAsync(It.Is<EntityIdentity>(id => id.Value == _testOfferEntityIdentity.Value)), Times.Once);
        }

        [Fact]
        public async Task It_should_call_set_requested_loan_amount_with_given_loan_amount()
        {
            // Given
            var requestedLoanAmount = new LoanAmount(TestRequestedAmount);
            var loanOffer = new Mock<ILoanOffer>();
            var repositoryMock = CreateLoanOfferRepositoryMock(_testOfferEntityIdentity, loanOffer);
            var command = CreateRequestLoanCommand();
            var handler = CreateRequestLoanCommandHandler(repositoryMock);

            // When
            await handler.Handle(command);

            // Then
            loanOffer.Verify(m => m.SetRequestedLoanAmount(It.Is<LoanAmount>(amount => amount.Value == requestedLoanAmount.Value)), Times.Once);
        }

        [Fact]
        public async Task It_should_call_update_in_repository_using_the_same_loan_offer()
        {
            // Given
            var requestedLoanAmount = new LoanAmount(TestRequestedAmount);
            var loanOffer = new Mock<ILoanOffer>();
            var repositoryMock = CreateLoanOfferRepositoryMock(_testOfferEntityIdentity, loanOffer);
            var command = CreateRequestLoanCommand();
            var handler = CreateRequestLoanCommandHandler(repositoryMock);

            // When
            await handler.Handle(command);

            // Then
            repositoryMock.Verify(m => m.UpdateAsync(loanOffer.Object), Times.Once);
        }

        [Fact]
        public async Task It_should_call_email_notification_service_with_parameters_from_loan_offer()
        {
            // Given
            var requestedLoanAmount = new LoanAmount(TestRequestedAmount);
            var loanOffer = new Mock<ILoanOffer>();
            var repositoryMock = CreateLoanOfferRepositoryMock(_testOfferEntityIdentity, loanOffer);
            var emailNotificationServiceMock = new Mock<IEmailNotificationService>();
            var command = CreateRequestLoanCommand();
            var handler = CreateRequestLoanCommandHandler(repositoryMock, emailNotificationServiceMock);

            // When
            await handler.Handle(command);

            // Then
            emailNotificationServiceMock.Verify(m => m.SendLoanRequestedMessage(loanOffer.Object.EmailAddress, loanOffer.Object.RequestedLoanAmount), Times.Once);
        }

        private RequestLoanCommand CreateRequestLoanCommand() => new RequestLoanCommand(_testOfferEntityIdentity.ToString(), TestRequestedAmount);

        private static Mock<ILoanOfferRepository> CreateLoanOfferRepositoryMock(EntityIdentity offerEntityIdentity, IMock<ILoanOffer> loanOfferMock = null)
        {
            var mock = new Mock<ILoanOfferRepository>();
            mock.Setup(m => m.GetAsync(It.Is<EntityIdentity>(id => id.Value == offerEntityIdentity.Value)))
                .ReturnsAsync(loanOfferMock?.Object ?? new Mock<ILoanOffer>().Object);
            return mock;
        }

        private static RequestLoanCommandHandler CreateRequestLoanCommandHandler(
            IMock<ILoanOfferRepository> loanOfferRepositoryMock = null,
            IMock<IEmailNotificationService> emailNotificationService = null
        )
            => new RequestLoanCommandHandler(
                loanOfferRepositoryMock?.Object ?? new Mock<ILoanOfferRepository>().Object,
                emailNotificationService?.Object ?? new Mock<IEmailNotificationService>().Object
            );
    }
}
