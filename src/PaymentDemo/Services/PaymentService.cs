using PaymentDemo.Models;
using PaymentDemo.Services.Payments;

namespace PaymentDemo.Services;

public class PaymentService
{
    private readonly IPaymentRepository _repo;
    private readonly IPaymentGatewayFactory _factory;

    public PaymentService(IPaymentRepository repo, IPaymentGatewayFactory factory)
    {
        _repo = repo;
        _factory = factory;
    }

    public async Task<string> StartPaymentAsync(PaymentRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.OrderId))
            throw new ArgumentException("OrderId is required.");

        if (request.Amount <= 0)
            throw new ArgumentException("Amount must be greater than 0.");

        // If same order exists, redirect to status page (simple idempotency)
        var existing = await _repo.GetByOrderIdAsync(request.OrderId);
        if (existing is not null)
            return "/Payments/Status?orderId=" + Uri.EscapeDataString(request.OrderId);

        // Create transaction first
        var tx = await _repo.CreateAsync(new PaymentTransaction
        {
            OrderId = request.OrderId,
            Amount = request.Amount,
            Currency = request.Currency,
            Status = "Pending",
            Provider = request.GatewayType.ToString()
        });

        // Select gateway using factory pattern
        var gateway = _factory.GetGateway(request.GatewayType);

        // Initialize provider
        var init = await gateway.InitializeAsync(request);

        await _repo.UpdateStatusAsync(tx.Id, "Pending", init.ProviderRef);

        // Redirect user to provider checkout
        return init.RedirectUrl;
    }
}
