namespace PaymentDemo.Services.Payments;

public class SslCommerzGateway : IPaymentGateway
{
    public PaymentGatewayType Type => PaymentGatewayType.SslCommerz;

    public Task<PaymentInitResult> InitializeAsync(PaymentRequest request)
    {
        // TODO: Replace with real SSLCommerz init call (GatewayPageURL)
        var providerRef = $"SSLC-{Guid.NewGuid():N}";
        var redirectUrl = $"/Payments/MockCheckout?orderId={Uri.EscapeDataString(request.OrderId)}&ref={providerRef}&provider=SSLCommerz";
        return Task.FromResult(new PaymentInitResult("SSLCommerz", providerRef, redirectUrl));
    }
}
