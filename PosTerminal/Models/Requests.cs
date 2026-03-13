namespace PosTerminal.Models;

public sealed record SaleRequest(decimal Amount, string Currency, string ReceiptNumber);
public sealed record CashierLoginRequest(string Username, string Password);
