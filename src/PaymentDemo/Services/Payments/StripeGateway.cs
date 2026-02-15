namespace PaymentDemo.Services.Payments;

public class StripeGateway : IPaymentGateway
{
    public PaymentGatewayType Type => PaymentGatewayType.Stripe;

    public Task<PaymentInitResult> InitializeAsync(PaymentRequest request)
    {
        // TODO: Replace with real Stripe Checkout Session creation
        var providerRef = $"STRIPE-{Guid.NewGuid():N}";
        var redirectUrl = $"/Payments/MockCheckout?orderId={Uri.EscapeDataString(request.OrderId)}&ref={providerRef}&provider=Stripe";
        return Task.FromResult(new PaymentInitResult("Stripe", providerRef, redirectUrl));
    }
}
