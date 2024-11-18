using AIRWIZZ.Data;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AirwizzContext>(options =>
    //options.UseSqlServer("Data Source=DESKTOP-3A7NVU7\\SQLEXPRESS;Initial Catalog=AirWizzDB;Integrated Security=True;Pooling=False;Encrypt=False;Trust Server Certificate=True"));
    options.UseSqlServer("Server=DESKTOP-2QHVFM9\\SQLEXPRESS;Database=AirWizzDB;Integrated Security=SSPI;TrustServerCertificate=True;"));



var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
