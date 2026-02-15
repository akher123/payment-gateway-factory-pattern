namespace PaymentDemo.Services.Payments;

public record PaymentRequest(
    string OrderId,
    decimal Amount,
    string Currency,
    string CustomerEmail,
    PaymentGatewayType GatewayType
);

public record PaymentInitResult(
    string Provider,
    string ProviderRef,
    string RedirectUrl
);
