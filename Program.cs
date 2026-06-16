using MLGroupShop.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

var isDevelopment = string.Equals(
    Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
    "Development",
    StringComparison.OrdinalIgnoreCase);

var builder = WebApplication.CreateBuilder(args);

var launchUrls = (Environment.GetEnvironmentVariable("ASPNETCORE_URLS") ?? string.Empty)
    .Split(';', StringSplitOptions.RemoveEmptyEntries)
    .Select(url => url.Trim())
    .Where(url => !string.IsNullOrWhiteSpace(url))
    .ToArray();

var launchUrl = launchUrls.FirstOrDefault(url =>
        url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
    ?? launchUrls.FirstOrDefault(url =>
        url.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
    ?? "http://localhost:5000";

var useHttpsRedirection = isDevelopment;

builder.Services.AddDbContext<AppDbContext>(options =>
   options.UseSqlite("Data Source=shop.db"));

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", config =>
    {
        config.LoginPath = "/Account/Login";

        config.Cookie.Name = "MLGroupAuth";

        config.ExpireTimeSpan = TimeSpan.FromHours(12);

        config.SlidingExpiration = false;
    });

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.Urls.Add("http://0.0.0.0:8080");

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

if (useHttpsRedirection)
{
    app.UseHttpsRedirection();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");




app.Run();
