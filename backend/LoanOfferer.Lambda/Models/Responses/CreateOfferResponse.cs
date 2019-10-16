using System;
using System.Collections.Generic;
using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;

namespace LoanOfferer.Lambda.Models.Responses
{
    public class CreateOfferResponse : APIGatewayProxyResponse
    {
        private const string CorsHeaderName = "Access-Control-Allow-Origin";
        private const string CorsHeaderValue = "*";
        
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
            
            if (Headers == null)
            {
                Headers = new Dictionary<string, string>();
            }

            Headers.Add(CorsHeaderName, CorsHeaderValue);
        }

        public static CreateOfferResponse Success(Guid id, int maxLoanAmount) => new CreateOfferResponse(id, maxLoanAmount);
    }
}
