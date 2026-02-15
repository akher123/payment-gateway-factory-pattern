using Microsoft.EntityFrameworkCore;
using PaymentDemo.Data;
using PaymentDemo.Services;
using PaymentDemo.Services.Payments;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// EF Core (Scoped)
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=payments.db"));

// Repository + business service (Scoped)
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<PaymentService>();

// Gateways (Transient)
builder.Services.AddTransient<IPaymentGateway, StripeGateway>();
builder.Services.AddTransient<IPaymentGateway, SslCommerzGateway>();
builder.Services.AddTransient<IPaymentGateway, BkashGateway>();

// Factory (Scoped is safest if gateways later need scoped deps)
builder.Services.AddScoped<IPaymentGatewayFactory, PaymentGatewayFactory>();

var app = builder.Build();

// Auto-create DB for demo (use migrations in real projects)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.Run();
