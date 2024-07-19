using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VendasWeb.Data;
using VendasWeb.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("VendasWebContextConnection") ?? throw new InvalidOperationException("Connection string 'VendasWebContextConnection' not found.");

builder.Services.AddDbContext<VendasWebContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<VendasWebUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<VendasWebContext>()
    ;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
