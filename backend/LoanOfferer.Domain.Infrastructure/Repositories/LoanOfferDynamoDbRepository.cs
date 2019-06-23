using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using LoanOfferer.Domain.Entities;
using LoanOfferer.Domain.Factories;
using LoanOfferer.Domain.Infrastructure.Exceptions;
using LoanOfferer.Domain.Repositories;
using LoanOfferer.Domain.ValueObjects;

namespace LoanOfferer.Domain.Infrastructure.Repositories
{
    public class LoanOfferDynamoDbRepository : ILoanOfferRepository
    {
        private readonly ILoanOfferFactory _loanOfferFactory;
        private readonly AmazonDynamoDBClient _dynamoDbClient;
        private const string LoanOfferTableName = "LoanOffer";
        private const string IdDynamoFieldName = "Id";
        private const string PeselNumberDynamoFieldName = "PeselNumber";
        private const string EmailAddressDynamoFieldName = "EmailAddress";
        private const string MaxLoanAmountDynamoFieldName = "MaxLoanAmount";
        private const string RequestedLoanAmountDynamoFiledName = "RequestedLoanAmount";

        private static Dictionary<string, AttributeValue> GetDictionaryWithIdAttribute(EntityIdentity offerId)
            => new Dictionary<string, AttributeValue> { { IdDynamoFieldName, new AttributeValue { S = offerId.ToString() } } };

        public LoanOfferDynamoDbRepository(ILoanOfferFactory loanOfferFactory)
        {
            _loanOfferFactory = loanOfferFactory;
            _dynamoDbClient = new AmazonDynamoDBClient();
        }

        public async Task AddAsync(LoanOffer loanOffer)
        {
            var request = CreatePutLoanOfferRequest(loanOffer);
            var response = await _dynamoDbClient.PutItemAsync(request);

            if (response.HttpStatusCode != HttpStatusCode.OK)
            {
                throw new FailedToPutLoanOfferToDynamoDbException(response.HttpStatusCode);
            }
        }

        public async Task<LoanOffer> GetAsync(EntityIdentity offerId)
        {
            var request = CreateGetLoanOfferRequest(offerId);
            var response = await _dynamoDbClient.GetItemAsync(request);

            if (response.HttpStatusCode != HttpStatusCode.OK)
            {
                throw new FailedToGetLoanOfferFromDynamoDbException(response.HttpStatusCode);
            }

            var item = response.Item;
            return _loanOfferFactory.Create(
                item[IdDynamoFieldName].S,
                item[PeselNumberDynamoFieldName].S,
                item[EmailAddressDynamoFieldName].S,
                Int32.Parse(item[MaxLoanAmountDynamoFieldName].N)
            );
        }

        public async Task UpdateAsync(LoanOffer loanOffer)
        {
            var request = CreateUpdateItemRequest(loanOffer);
            var response = await _dynamoDbClient.UpdateItemAsync(request);

            if (response.HttpStatusCode != HttpStatusCode.OK)
            {
                throw new FailedToUpdateLoanOfferInDynamoDbException(response.HttpStatusCode);
            }
        }

        private static UpdateItemRequest CreateUpdateItemRequest(LoanOffer loanOffer)
            => new UpdateItemRequest
            {
                TableName = LoanOfferTableName,
                Key = GetDictionaryWithIdAttribute(loanOffer.Id),
                AttributeUpdates = new Dictionary<string, AttributeValueUpdate>
                {
                    { RequestedLoanAmountDynamoFiledName, new AttributeValueUpdate(new AttributeValue { N = loanOffer.RequestedLoanAmount.ToString() }, AttributeAction.PUT) }
                }
            };

        private static GetItemRequest CreateGetLoanOfferRequest(EntityIdentity offerId)
            => new GetItemRequest
            {
                TableName = LoanOfferTableName,
                Key = GetDictionaryWithIdAttribute(offerId),
                AttributesToGet = new List<string>
                {
                    IdDynamoFieldName,
                    PeselNumberDynamoFieldName,
                    EmailAddressDynamoFieldName,
                    MaxLoanAmountDynamoFieldName
                }
            };

        private static PutItemRequest CreatePutLoanOfferRequest(LoanOffer loanOffer)
            => new PutItemRequest
            {
                TableName = LoanOfferTableName,
                Item = new Dictionary<string, AttributeValue>
                {
                    { IdDynamoFieldName, new AttributeValue { S = loanOffer.Id.ToString() } },
                    { PeselNumberDynamoFieldName, new AttributeValue { S = loanOffer.PeselNumber.Value } },
                    { EmailAddressDynamoFieldName, new AttributeValue { S = loanOffer.EmailAddress.Value } },
                    { MaxLoanAmountDynamoFieldName, new AttributeValue { N = loanOffer.MaxLoanAmount.ToString() } },
                }
            };
    }
}
