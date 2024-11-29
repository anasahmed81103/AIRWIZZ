using AIRWIZZ.Data;
using AIRWIZZ.Service;
//using AIRWIZZ.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<FlightTrackingService>();               // Registration of service for real-time flight data

builder.Services.AddControllersWithViews();

// Register IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Add session services
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
    options.Cookie.HttpOnly = true;                // Make cookie accessible only to the server
    options.Cookie.IsEssential = true;             // Ensure cookie is created
});

builder.Services.AddDbContext<AirwizzContext>(options =>
    options.UseSqlServer("Server=DESKTOP-B9L8PKU\\SQLEXPRESS;Database=AirWizzDB;Integrated Security=SSPI;TrustServerCertificate=True;"));
    //options.UseSqlServer("server=desktop-2qhvfm9\\sqlexpress;database=airwizzdb;integrated security=sspi;trustservercertificate=true;"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Enable session middleware
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
