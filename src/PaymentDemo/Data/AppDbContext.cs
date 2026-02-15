using Microsoft.EntityFrameworkCore;
using PaymentDemo.Models;

namespace PaymentDemo.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<PaymentTransaction> PaymentTransactions => Set<PaymentTransaction>();
}
