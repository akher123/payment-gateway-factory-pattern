namespace PaymentDemo.Services.Payments;

public class PaymentGatewayFactory : IPaymentGatewayFactory
{
    private readonly IReadOnlyDictionary<PaymentGatewayType, IPaymentGateway> _map;

    public PaymentGatewayFactory(IEnumerable<IPaymentGateway> gateways)
    {
        _map = gateways.ToDictionary(g => g.Type, g => g);
    }

    public IPaymentGateway GetGateway(PaymentGatewayType type)
    {
        if (_map.TryGetValue(type, out var gateway))
            return gateway;

        throw new NotSupportedException($"Gateway '{type}' is not registered.");
    }
}
