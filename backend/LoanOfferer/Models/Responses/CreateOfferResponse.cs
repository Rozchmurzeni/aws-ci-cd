using System;
using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;

namespace LoanOfferer.Models.Responses
{
    public class CreateOfferResponse : APIGatewayProxyResponse
    {
        private CreateOfferResponse(Guid id, int maxLoanAmount)
        {
            StatusCode = (int) HttpStatusCode.Created;
            Body = JsonConvert.SerializeObject(
                new
                {
                    Id = id,
                    MaxLoanAmount = maxLoanAmount
                }
            );
        }

        public static CreateOfferResponse Success(Guid id, int maxLoanAmount) => new CreateOfferResponse(id, maxLoanAmount);
    }
}
