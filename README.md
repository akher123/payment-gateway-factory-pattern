# PaymentMultiGatewayDemo

ASP.NET Core Razor Pages demo project showing:
- Multiple payment gateways (Stripe / SSLCommerz / bKash) as mock implementations
- Dependency Injection + Factory Pattern to select gateways
- EF Core SQLite to store payment transactions
- Pages:
  - /Payments/Create
  - /Payments/MockCheckout
  - /Payments/Status

## Run
1. Open `PaymentMultiGatewayDemo.sln` in Visual Studio 2022+
2. Restore NuGet packages
3. Run the `PaymentDemo` project
4. Go to `/Payments/Create`

> Note: For simplicity this demo uses `db.Database.EnsureCreated()` (no migrations).
