using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using LoanOfferer.Domain.Services;
using LoanOfferer.Domain.ValueObjects;

namespace LoanOfferer.Domain.Infrastructure.Services
{
    public class SesEmailNotificationService : IEmailNotificationService
    {
        private readonly IAmazonSimpleEmailService _amazonSimpleEmailService;
        private const string LoanRequestedEmailSubject = "Thank you!";
        private const string SourceEmail = "ci.cd.workshops@gmail.com";
        private const string MessageBodyHtmlResourceName = "LoanOfferer.Domain.Infrastructure.Resources.loan-offerer-email-template.html";
        private const string LoanAmountHtmlPlaceholder = "{{LoanAmount}}";

        public SesEmailNotificationService()
        {
            _amazonSimpleEmailService = new AmazonSimpleEmailServiceClient(RegionEndpoint.EUWest1);
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
                Source = SourceEmail,
                Destination = new Destination { ToAddresses = new List<string> { emailAddress.Value } },
                Message = new Message
                {
                    Subject = new Content { Data = LoanRequestedEmailSubject },
                    Body = new Body
                    {
                        Html = new Content
                        {
                            Data = GetEmailBody()
                               .Replace(
                                    LoanAmountHtmlPlaceholder,
                                    requestedLoanAmount.Value.ToString()
                                )
                        }
                    }
                }
            };

        private static string GetEmailBody()
        {
            var assembly = typeof(SesEmailNotificationService).GetTypeInfo().Assembly;
            var htmlStream = assembly.GetManifestResourceStream(MessageBodyHtmlResourceName);

            using (var streamReader = new StreamReader(htmlStream))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
