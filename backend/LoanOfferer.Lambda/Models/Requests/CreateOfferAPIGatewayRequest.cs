using Amazon.Lambda.APIGatewayEvents;
using LoanOfferer.Contracts.Requests;
using Newtonsoft.Json;

namespace LoanOfferer.Lambda.Models.Requests
{
    public class CreateOfferAPIGatewayRequest : APIGatewayProxyRequest
    {
        private CreateOfferRequest _requestBody;
        private CreateOfferRequest RequestBody => _requestBody ?? (_requestBody = JsonConvert.DeserializeObject<CreateOfferRequest>(Body));

        public string PeselNumber => RequestBody.PeselNumber;
        public string EmailAddress => RequestBody.EmailAddress;
    }
}
