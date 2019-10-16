using System.Collections.Generic;
using System.IO;
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
        private readonly IEmailNotificationServiceConfig _config;
        private readonly IAmazonSimpleEmailService _amazonSimpleEmailService;

        private const string LoanRequestedEmailSubject = "Thank you!";
        private const string SourceEmail = "ci.cd.workshops@gmail.com";
        private const string MessageBodyHtmlResourceName = "LoanOfferer.Lambda.Domain.Infrastructure.Resources.loan-offerer-email-template.html";

        private const string LoanAmountHtmlPlaceholder = "{{LoanAmount}}";
        private const string FrontendApplicationUrlHtmlPlaceholder = "{{FrontendApplicationUrl}}";

        public SesEmailNotificationService(IEmailNotificationServiceConfig config)
        {
            _config = config;
            _amazonSimpleEmailService = new AmazonSimpleEmailServiceClient(RegionEndpoint.EUWest1);
        }

        public async Task SendLoanRequestedMessage(EmailAddress emailAddress, LoanAmount requestedLoanAmount)
        {
            var request = CreateSendEmailRequest(emailAddress, requestedLoanAmount);
            await _amazonSimpleEmailService.SendEmailAsync(request);
        }

        private SendEmailRequest CreateSendEmailRequest(EmailAddress emailAddress, LoanAmount requestedLoanAmount)
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
                                  .Replace(
                                       FrontendApplicationUrlHtmlPlaceholder,
                                       _config.FrontendApplicationUrl
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
