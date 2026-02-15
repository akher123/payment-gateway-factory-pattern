using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentDemo.Services;
using PaymentDemo.Services.Payments;

namespace PaymentDemo.Pages.Payments;

public class CreateModel : PageModel
{
    private readonly PaymentService _paymentService;

    public CreateModel(PaymentService paymentService) => _paymentService = paymentService;

    [BindProperty] public string OrderId { get; set; } = "";
    [BindProperty] public decimal Amount { get; set; }
    [BindProperty] public string Currency { get; set; } = "BDT";
    [BindProperty] public string CustomerEmail { get; set; } = "";
    [BindProperty] public PaymentGatewayType GatewayType { get; set; } = PaymentGatewayType.SslCommerz;

    public string? Error { get; set; }

    public void OnGet()
    {
        if (string.IsNullOrWhiteSpace(OrderId))
            OrderId = "ORDER-" + DateTime.UtcNow.ToString("yyyyMMdd-HHmmss");
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            var req = new PaymentRequest(OrderId, Amount, Currency, CustomerEmail, GatewayType);
            var redirectUrl = await _paymentService.StartPaymentAsync(req);
            return Redirect(redirectUrl);
        }
        catch (Exception ex)
        {
            Error = ex.Message;
            return Page();
        }
    }
}
