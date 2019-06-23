using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;

namespace LoanOfferer.Models.Requests
{
    public class RequestLoanRequest : APIGatewayProxyRequest
    {
        private RequestLoanBodyRequest _requestBody;
        private RequestLoanBodyRequest RequestBody => _requestBody ?? (_requestBody = JsonConvert.DeserializeObject<RequestLoanBodyRequest>(Body));

        public string OfferId => RequestBody.OfferId;
        public int RequestedAmount => RequestBody.RequestedAmount;

        private class RequestLoanBodyRequest
        {
            public string OfferId { get; set; }
            public int RequestedAmount { get; set; }
        }
    }
}
