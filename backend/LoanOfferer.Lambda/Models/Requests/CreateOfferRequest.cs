using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;

namespace LoanOfferer.Lambda.Models.Requests
{
    public class CreateOfferRequest : APIGatewayProxyRequest
    {
        private CreateOfferBodyRequest _requestBody;
        private CreateOfferBodyRequest RequestBody => _requestBody ?? (_requestBody = JsonConvert.DeserializeObject<CreateOfferBodyRequest>(Body));

        public string PeselNumber => RequestBody.PeselNumber;
        public string EmailAddress => RequestBody.EmailAddress;

        private class CreateOfferBodyRequest
        {
            public string PeselNumber { get; set; }
            public string EmailAddress { get; set; }
        }
    }
}
