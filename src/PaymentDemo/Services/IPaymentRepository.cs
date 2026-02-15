using PaymentDemo.Models;

namespace PaymentDemo.Services;

public interface IPaymentRepository
{
    Task<PaymentTransaction> CreateAsync(PaymentTransaction tx);
    Task<PaymentTransaction?> GetByOrderIdAsync(string orderId);
    Task UpdateStatusAsync(int id, string status, string providerRef);
}
