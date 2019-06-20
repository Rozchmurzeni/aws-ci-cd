using Amazon.Lambda.APIGatewayEvents;

namespace LoanOfferer.Models.Requests
{
    public class CreateOfferRequest : APIGatewayProxyRequest
    {
        public string PeselNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}
