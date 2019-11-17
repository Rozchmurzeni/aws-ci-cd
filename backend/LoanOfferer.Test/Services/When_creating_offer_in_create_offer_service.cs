using System.Threading.Tasks;
using FluentAssertions;
using LoanOfferer.CommandHandlers;
using LoanOfferer.Commands;
using LoanOfferer.Domain.Entities;
using LoanOfferer.Domain.Factories;
using LoanOfferer.Domain.Repositories;
using LoanOfferer.Domain.Services;
using Moq;
using Xunit;

namespace LoanOfferer.Test.Services
{
    public class When_creating_offer_in_create_offer_service
    {
        private const string PeselNumber = "testPeselNumber";
        private const string EmailAddress = "testEmailAddress";

        [Fact]
        public async Task It_should_create_offer_entity_using_offer_factory()
        {
            // Given
            var factoryMock = CreateConfiguredLoanOfferFactoryMock();
            var command = CreateTestCommand();
            var handler = CreateCreateOfferCommandHandler(factoryMock);

            // When
            await handler.Handle(command);

            // Then
            factoryMock.Verify(mock => mock.Create(PeselNumber, EmailAddress), Times.Once);
        }

        [Fact]
        public async Task It_should_add_created_loan_offer_entity_to_repository()
        {
            // Given
            var loanOfferMock = new Mock<ILoanOffer>();
            var repositoryMock = new Mock<ILoanOfferRepository>();
            var factoryMock = CreateConfiguredLoanOfferFactoryMock(loanOfferMock);
            var command = CreateTestCommand();
            var handler = CreateCreateOfferCommandHandler(factoryMock, loanOfferRepositoryMock: repositoryMock);

            // When
            await handler.Handle(command);

            // Then
            repositoryMock.Verify(mock => mock.AddAsync(loanOfferMock.Object), Times.Once);
        }

        [Fact]
        public async Task It_should_return_created_loan_offer_entity()
        {
            // Given
            var loanOfferMock = new Mock<ILoanOffer>();
            var factoryMock = CreateConfiguredLoanOfferFactoryMock(loanOfferMock);
            var command = CreateTestCommand();
            var handler = CreateCreateOfferCommandHandler(factoryMock);

            // When
            var result = await handler.Handle(command);

            // Then
            result.Should().Be(loanOfferMock.Object);
        }

        [Fact]
        public async Task It_should_call_get_score_in_scoring_service()
        {
            // Given
            var loanOfferMock = new Mock<ILoanOffer>();
            var scoringServiceMock = new Mock<IScoringService>();
            var factoryMock = CreateConfiguredLoanOfferFactoryMock(loanOfferMock);
            var command = CreateTestCommand();
            var handler = CreateCreateOfferCommandHandler(factoryMock, scoringServiceMock);

            // When
            await handler.Handle(command);

            // Then
            scoringServiceMock.Verify(mock => mock.GetScoreAsync(loanOfferMock.Object.PeselNumber), Times.Once);
        }

        private static CreateOfferCommand CreateTestCommand() => new CreateOfferCommand(PeselNumber, EmailAddress);

        private static Mock<ILoanOfferFactory> CreateConfiguredLoanOfferFactoryMock(IMock<ILoanOffer> loanOfferMock = null)
        {
            var mock = new Mock<ILoanOfferFactory>();
            mock.Setup(m => m.Create(PeselNumber, EmailAddress))
                .Returns(loanOfferMock?.Object ?? new Mock<ILoanOffer>().Object);
            return mock;
        }

        private static CreateOfferCommandHandler CreateCreateOfferCommandHandler(
            IMock<ILoanOfferFactory> loanOfferFactoryMock = null,
            IMock<IScoringService> scoringServiceMock = null,
            IMock<ILoanOfferRepository> loanOfferRepositoryMock = null
        )
            => new CreateOfferCommandHandler(
                loanOfferFactoryMock?.Object ?? new Mock<ILoanOfferFactory>().Object,
                loanOfferRepositoryMock?.Object ?? new Mock<ILoanOfferRepository>().Object,
                scoringServiceMock?.Object ?? new Mock<IScoringService>().Object
            );
    }
}
