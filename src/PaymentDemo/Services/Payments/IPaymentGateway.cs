namespace PaymentDemo.Services.Payments;

public interface IPaymentGateway
{
    PaymentGatewayType Type { get; }
    Task<PaymentInitResult> InitializeAsync(PaymentRequest request);
}
