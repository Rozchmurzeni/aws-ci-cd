using System.Collections.Generic;
using System.Net;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using LoanOfferer.Domain.Entities;
using LoanOfferer.Domain.Infrastructure.Exceptions;
using LoanOfferer.Domain.Repositories;
using LoanOfferer.Domain.Snapshots;

namespace LoanOfferer.Domain.Infrastructure.Repositories
{
    public class LoanOfferDynamoDbRepository : ILoanOfferRepository
    {
        private readonly AmazonDynamoDBClient _dynamoDbClient;
        private const string LoanOfferTableName = "LoanOffer";

        public LoanOfferDynamoDbRepository()
        {
            _dynamoDbClient = new AmazonDynamoDBClient();
        }

        public void Add(LoanOffer loanOffer)
        {
            var snapshot = loanOffer.GetSnapshot();
            var request = CreatePutLoanOfferRequest(snapshot);
            var result = _dynamoDbClient.PutItemAsync(request).GetAwaiter().GetResult();

            if (result.HttpStatusCode != HttpStatusCode.OK)
            {
                throw new FailedToPutLoanOfferToDynamoDb(result.HttpStatusCode);
            }
        }

        private static PutItemRequest CreatePutLoanOfferRequest(LoanOfferSnapshot snapshot)
            => new PutItemRequest
            {
                TableName = LoanOfferTableName,
                Item = new Dictionary<string, AttributeValue>
                {
                    { "Id", new AttributeValue { S = snapshot.Id.ToString() } },
                    { "Id", new AttributeValue { S = snapshot.PeselNumber } },
                    { "Id", new AttributeValue { S = snapshot.EmailAddress } },
                    { "Id", new AttributeValue { N = snapshot.MaxLoanAmount.ToString() } },
                }
            };
    }
}
