using LoanOfferer.Domain.Entities;
using LoanOfferer.Domain.Factories;
using LoanOfferer.Domain.Repositories;
using LoanOfferer.Domain.Services;
using LoanOfferer.Services;
using Moq;

namespace LoanOfferer.Test.Helpers.Builders
{
    internal class CreateOfferServiceBuilder
    {
        public CreateOfferServiceBuilder()
        {
            LoanOfferFactoryMock = new Mock<ILoanOfferFactory>();
            LoanOfferRepositoryMock = new Mock<ILoanOfferRepository>();
            ScoringServiceMock = new Mock<IScoringService>();
            LoanOfferMock = new Mock<ILoanOffer>();
        }

        internal Mock<ILoanOfferFactory> LoanOfferFactoryMock { get; }
        internal Mock<ILoanOffer> LoanOfferMock { get; }
        internal Mock<ILoanOfferRepository> LoanOfferRepositoryMock { get; }
        internal Mock<IScoringService> ScoringServiceMock { get; }

        internal CreateOfferService Build() => new CreateOfferService(LoanOfferFactoryMock.Object, LoanOfferRepositoryMock.Object, ScoringServiceMock.Object);

        public CreateOfferServiceBuilder WithLoanOfferFactoryReturningLoanOffer(string peselNumber, string emailAddress)
        {
            LoanOfferFactoryMock.Setup(mock => mock.Create(peselNumber, emailAddress))
                                .Returns(LoanOfferMock.Object);
            return this;
        }
    }
}
