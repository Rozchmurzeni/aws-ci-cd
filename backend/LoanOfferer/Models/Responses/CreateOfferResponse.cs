using System;
using Amazon.Lambda.APIGatewayEvents;

namespace LoanOfferer.Models.Responses
{
    public class CreateOfferResponse : APIGatewayProxyResponse
    {
        public CreateOfferResponse(Guid id, int maxValue) {}
    }
}
