namespace LoanOfferer.Commands
{
    public class CreateOfferCommand
    {
        public string PeselNumber { get; }
        public string EmailAddress { get; }

        public CreateOfferCommand(string peselNumber, string emailAddress)
        {
            PeselNumber = peselNumber;
            EmailAddress = emailAddress;
        }
    }
}
