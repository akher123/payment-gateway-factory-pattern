using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PaymentDemo.Pages.Payments;

public class MockCheckoutModel : PageModel
{
    [BindProperty(SupportsGet = true)] public string OrderId { get; set; } = "";
    [BindProperty(SupportsGet = true)] public string Ref { get; set; } = "";
    [BindProperty(SupportsGet = true)] public string Provider { get; set; } = "";

    public IActionResult OnPostPaid()
        => RedirectToPage("/Payments/Status", new { orderId = OrderId, status = "Paid", providerRef = Ref });

    public IActionResult OnPostFailed()
        => RedirectToPage("/Payments/Status", new { orderId = OrderId, status = "Failed", providerRef = Ref });
}
