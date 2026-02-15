namespace PaymentDemo.Services.Payments;

public interface IPaymentGatewayFactory
{
    IPaymentGateway GetGateway(PaymentGatewayType type);
}
