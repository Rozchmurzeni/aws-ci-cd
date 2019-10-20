namespace LoanOfferer.Contracts.Requests
{
    public class RequestLoanRequest
    {
        public string OfferId { get; set; }
        public int RequestedAmount { get; set; }
    }
}
