using System.Collections.Generic;
using System.Net;
using Amazon.Lambda.APIGatewayEvents;

namespace LoanOfferer.Lambda.Models.Responses
{
    public class RequestLoanResponse : APIGatewayProxyResponse
    {
        private const string CorsHeaderName = "Access-Control-Allow-Origin";
        private const string CorsHeaderValue = "*";

        private RequestLoanResponse()
        {
            StatusCode = (int) HttpStatusCode.OK;

            if (Headers == null)
            {
                Headers = new Dictionary<string, string>();
            }

            Headers.Add(CorsHeaderName, CorsHeaderValue);
        }

        public static RequestLoanResponse Success => new RequestLoanResponse();
    }
}
