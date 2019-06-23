using System.Net;
using Amazon.Lambda.APIGatewayEvents;

namespace LoanOfferer.Models.Responses
{
    public class RequestLoanResponse : APIGatewayProxyResponse
    {
        private RequestLoanResponse()
        {
            StatusCode = (int) HttpStatusCode.OK;
        }

        public static RequestLoanResponse Success => new RequestLoanResponse();
    }
}
