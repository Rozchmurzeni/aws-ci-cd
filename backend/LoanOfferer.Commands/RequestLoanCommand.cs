namespace LoanOfferer.Commands
{
    public class RequestLoanCommand
    {
        public string OfferId { get; }
        public int RequestedAmount { get; }

        public RequestLoanCommand(string offerId, int requestedAmount)
        {
            OfferId = offerId;
            RequestedAmount = requestedAmount;
        }
    }
}
