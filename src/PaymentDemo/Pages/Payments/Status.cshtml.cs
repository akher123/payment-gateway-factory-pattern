using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaymentDemo.Services;

namespace PaymentDemo.Pages.Payments;

public class StatusModel : PageModel
{
    private readonly IPaymentRepository _repo;

    public StatusModel(IPaymentRepository repo) => _repo = repo;

    [BindProperty(SupportsGet = true)] public string OrderId { get; set; } = "";
    [BindProperty(SupportsGet = true)] public string? Status { get; set; }
    [BindProperty(SupportsGet = true)] public string? ProviderRefQuery { get; set; } // optional alias

    [BindProperty(SupportsGet = true, Name = "providerRef")] public string? ProviderRef { get; set; }

    public bool NotFound { get; set; }
    public string CurrentStatus { get; set; } = "Unknown";
    public string Provider { get; set; } = "";
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "";
    public DateTime CreatedAtUtc { get; set; }

    public async Task OnGetAsync()
    {
        var tx = await _repo.GetByOrderIdAsync(OrderId);
        if (tx is null)
        {
            NotFound = true;
            return;
        }

        // If a mock provider returned status, update it
        var providerRef = ProviderRef ?? ProviderRefQuery;

        if (!string.IsNullOrWhiteSpace(Status) && !string.IsNullOrWhiteSpace(providerRef))
        {
            await _repo.UpdateStatusAsync(tx.Id, Status!, providerRef!);
            tx = await _repo.GetByOrderIdAsync(OrderId);
        }

        if (tx is null)
        {
            NotFound = true;
            return;
        }

        CurrentStatus = tx.Status;
        Provider = tx.Provider;
        ProviderRef = tx.ProviderRef;
        Amount = tx.Amount;
        Currency = tx.Currency;
        CreatedAtUtc = tx.CreatedAtUtc;
    }
}
