namespace PaymentDemo.Models;

public class PaymentTransaction
{
    public int Id { get; set; }
    public string OrderId { get; set; } = "";
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "BDT";
    public string Status { get; set; } = "Created"; // Created, Pending, Paid, Failed
    public string Provider { get; set; } = "Unknown";
    public string ProviderRef { get; set; } = "";
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
