using Amazon.Lambda.APIGatewayEvents;
using LoanOfferer.Commands;
using LoanOfferer.Contracts.Requests;
using Newtonsoft.Json;

namespace LoanOfferer.Lambda.Models.Requests
{
    public class RequestLoanAPIGatewayRequest : APIGatewayProxyRequest
    {
        private RequestLoanRequest _requestBody;
        private RequestLoanRequest RequestBody => _requestBody ?? (_requestBody = JsonConvert.DeserializeObject<RequestLoanRequest>(Body));

        public string OfferId => RequestBody.OfferId;
        public int RequestedAmount => RequestBody.RequestedAmount;

        public RequestLoanCommand ToRequestLoanCommand() => new RequestLoanCommand(OfferId, RequestedAmount);
    }
}
