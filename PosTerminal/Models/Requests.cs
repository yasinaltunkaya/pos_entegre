namespace PosTerminal.Models
{
    public sealed class SaleRequest
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string ReceiptNumber { get; set; }
    }

    public sealed class CashierLoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

