using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using LoanOfferer.Domain.Services;
using LoanOfferer.Domain.ValueObjects;

namespace LoanOfferer.Domain.Infrastructure.Services
{
    public class SesEmailNotificationService : IEmailNotificationService
    {
        private readonly IAmazonSimpleEmailService _amazonSimpleEmailService;
        private const string LoanRequestedEmailSubject = "Congratulations!";
        private const string MessageTemplate = "{0}";

        public SesEmailNotificationService()
        {
            _amazonSimpleEmailService = new AmazonSimpleEmailServiceClient();
        }

        public async Task SendLoanRequestedMessage(EmailAddress emailAddress, LoanAmount requestedLoanAmount)
        {
            var request = CreateSendEmailRequest(emailAddress, requestedLoanAmount);
            var response = await _amazonSimpleEmailService.SendEmailAsync(request);

            if (response.HttpStatusCode != HttpStatusCode.OK)
            {
                // TODO: Log information about failure
            }
        }

        private static SendEmailRequest CreateSendEmailRequest(EmailAddress emailAddress, LoanAmount requestedLoanAmount)
            => new SendEmailRequest
            {
                Destination = new Destination { ToAddresses = new List<string> { emailAddress.Value } },
                Message = new Message
                {
                    Subject = new Content { Data = LoanRequestedEmailSubject },
                    Body = new Body { Html = new Content { Data = String.Format(MessageTemplate, requestedLoanAmount.Value) } }
                }
            };
    }
}
