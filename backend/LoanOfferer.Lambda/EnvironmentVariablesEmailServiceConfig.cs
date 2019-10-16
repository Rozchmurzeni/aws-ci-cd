using System;
using LoanOfferer.Domain.Infrastructure.Services;

namespace LoanOfferer.Lambda
{
    public class EnvironmentVariablesEmailServiceConfig : IEmailNotificationServiceConfig
    {
        public string FrontendApplicationUrl => Environment.GetEnvironmentVariable("FrontendApplicationUrl");
    }
}
