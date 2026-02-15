namespace PaymentDemo.Services.Payments;

public class BkashGateway : IPaymentGateway
{
    public PaymentGatewayType Type => PaymentGatewayType.Bkash;

    public Task<PaymentInitResult> InitializeAsync(PaymentRequest request)
    {
        // TODO: Replace with real bKash create payment call
        var providerRef = $"BKASH-{Guid.NewGuid():N}";
        var redirectUrl = $"/Payments/MockCheckout?orderId={Uri.EscapeDataString(request.OrderId)}&ref={providerRef}&provider=bKash";
        return Task.FromResult(new PaymentInitResult("bKash", providerRef, redirectUrl));
    }
}
