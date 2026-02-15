using Microsoft.EntityFrameworkCore;
using PaymentDemo.Data;
using PaymentDemo.Models;

namespace PaymentDemo.Services;

public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _db;
    public PaymentRepository(AppDbContext db) => _db = db;

    public async Task<PaymentTransaction> CreateAsync(PaymentTransaction tx)
    {
        _db.PaymentTransactions.Add(tx);
        await _db.SaveChangesAsync();
        return tx;
    }

    public Task<PaymentTransaction?> GetByOrderIdAsync(string orderId)
        => _db.PaymentTransactions.AsNoTracking().FirstOrDefaultAsync(x => x.OrderId == orderId);

    public async Task UpdateStatusAsync(int id, string status, string providerRef)
    {
        var tx = await _db.PaymentTransactions.FirstAsync(x => x.Id == id);
        tx.Status = status;
        tx.ProviderRef = providerRef;
        await _db.SaveChangesAsync();
    }
}
