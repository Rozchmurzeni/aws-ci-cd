using System.Threading.Tasks;
using FluentAssertions;
using LoanOfferer.Domain.Entities;
using LoanOfferer.Domain.Factories;
using LoanOfferer.Domain.Repositories;
using LoanOfferer.Domain.Services;
using LoanOfferer.Services;
using Moq;
using Xunit;

namespace LoanOfferer.Test.Services
{
    public class When_creating_offer_in_create_offer_service
    {
        [Fact]
        public async Task It_should_create_offer_entity_using_offer_factory()
        {
            // Given
            const string peselNumber = "testPeselNumber";
            const string emailAddress = "testEmailAddress";
            var factoryMock = CreateConfiguredLoanOfferFactoryMock(peselNumber, emailAddress);
            var service = CreateOfferService(factoryMock);

            // When
            await service.CreateOfferAsync(peselNumber, emailAddress);

            // Then
            factoryMock.Verify(mock => mock.Create(peselNumber, emailAddress), Times.Once);
        }

        [Fact]
        public async Task It_should_add_created_loan_offer_entity_to_repository()
        {
            // Given
            const string peselNumber = "testPeselNumber";
            const string emailAddress = "testEmailAddress";
            var loanOfferMock = new Mock<ILoanOffer>();
            var repositoryMock = new Mock<ILoanOfferRepository>();
            var factoryMock = CreateConfiguredLoanOfferFactoryMock(peselNumber, emailAddress, loanOfferMock);
            var service = CreateOfferService(factoryMock, loanOfferRepositoryMock: repositoryMock);

            // When
            await service.CreateOfferAsync(peselNumber, emailAddress);

            // Then
            repositoryMock.Verify(mock => mock.AddAsync(loanOfferMock.Object), Times.Once);
        }

        [Fact]
        public async Task It_should_return_created_loan_offer_entity()
        {
            // Given
            const string peselNumber = "testPeselNumber";
            const string emailAddress = "testEmailAddress";
            var loanOfferMock = new Mock<ILoanOffer>();
            var factoryMock = CreateConfiguredLoanOfferFactoryMock(peselNumber, emailAddress, loanOfferMock);
            var service = CreateOfferService(factoryMock);

            // When
            var result = await service.CreateOfferAsync(peselNumber, emailAddress);

            // Then
            result.Should().Be(loanOfferMock.Object);
        }

        [Fact]
        public async Task It_should_call_get_score_in_scoring_service()
        {
            // Given
            const string peselNumber = "testPeselNumber";
            const string emailAddress = "testEmailAddress";
            var loanOfferMock = new Mock<ILoanOffer>();
            var scoringServiceMock = new Mock<IScoringService>();
            var factoryMock = CreateConfiguredLoanOfferFactoryMock(peselNumber, emailAddress, loanOfferMock);
            var service = CreateOfferService(factoryMock, scoringServiceMock);

            // When
            await service.CreateOfferAsync(peselNumber, emailAddress);

            // Then
            scoringServiceMock.Verify(mock => mock.GetScoreAsync(loanOfferMock.Object.PeselNumber), Times.Once);
        }

        private static Mock<ILoanOfferFactory> CreateConfiguredLoanOfferFactoryMock(
            string peselNumber,
            string emailAddress,
            IMock<ILoanOffer> loanOfferMock = null
        )
        {
            var mock = new Mock<ILoanOfferFactory>();
            mock.Setup(m => m.Create(peselNumber, emailAddress))
                .Returns(loanOfferMock?.Object ?? new Mock<ILoanOffer>().Object);
            return mock;
        }

        private static CreateOfferService CreateOfferService(
            IMock<ILoanOfferFactory> loanOfferFactoryMock = null,
            IMock<IScoringService> scoringServiceMock = null,
            IMock<ILoanOfferRepository> loanOfferRepositoryMock = null
        )
            => new CreateOfferService(
                loanOfferFactoryMock?.Object ?? new Mock<ILoanOfferFactory>().Object,
                loanOfferRepositoryMock?.Object ?? new Mock<ILoanOfferRepository>().Object,
                scoringServiceMock?.Object ?? new Mock<IScoringService>().Object
            );
    }
}
