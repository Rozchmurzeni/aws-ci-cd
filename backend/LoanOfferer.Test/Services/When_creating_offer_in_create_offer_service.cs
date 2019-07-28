using System.Threading.Tasks;
using FluentAssertions;
using LoanOfferer.Test.Helpers.Builders;
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
            var serviceBuilder = new CreateOfferServiceBuilder();
            var service = serviceBuilder
                         .WithLoanOfferFactoryReturningLoanOffer(peselNumber, emailAddress)
                         .Build();

            // When
            await service.CreateOfferAsync(peselNumber, emailAddress);

            // Then
            serviceBuilder.LoanOfferFactoryMock.Verify(mock => mock.Create(peselNumber, emailAddress), Times.Once);
        }

        [Fact]
        public async Task It_should_call_calculate_offer_in_created_loan_offer_entity()
        {
            // Given
            const string peselNumber = "testPeselNumber";
            const string emailAddress = "testEmailAddress";
            var serviceBuilder = new CreateOfferServiceBuilder();
            var service = serviceBuilder
                         .WithLoanOfferFactoryReturningLoanOffer(peselNumber, emailAddress)
                         .Build();

            // When
            await service.CreateOfferAsync(peselNumber, emailAddress);

            // Then
            serviceBuilder.LoanOfferMock.Verify(mock => mock.CalculateOffer(serviceBuilder.ScoringServiceMock.Object), Times.Once);
        }

        [Fact]
        public async Task It_should_add_created_loan_offer_entity_to_repository()
        {
            // Given
            const string peselNumber = "testPeselNumber";
            const string emailAddress = "testEmailAddress";
            var serviceBuilder = new CreateOfferServiceBuilder();
            var service = serviceBuilder
                         .WithLoanOfferFactoryReturningLoanOffer(peselNumber, emailAddress)
                         .Build();

            // When
            await service.CreateOfferAsync(peselNumber, emailAddress);

            // Then
            serviceBuilder.LoanOfferRepositoryMock.Verify(mock => mock.AddAsync(serviceBuilder.LoanOfferMock.Object), Times.Once);
        }

        [Fact]
        public async Task It_should_return_created_loan_offer_entity()
        {
            // Given
            const string peselNumber = "testPeselNumber";
            const string emailAddress = "testEmailAddress";
            var serviceBuilder = new CreateOfferServiceBuilder();
            var service = serviceBuilder
                         .WithLoanOfferFactoryReturningLoanOffer(peselNumber, emailAddress)
                         .Build();

            // When
            var result = await service.CreateOfferAsync(peselNumber, emailAddress);

            // Then
            result.Should().Be(serviceBuilder.LoanOfferMock.Object);
        }
    }
}
