using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Localization;
using SalesWebApp.Data;
using SalesWebApp.Models;
using SalesWebApp.Services;


var builder = WebApplication.CreateBuilder(args);

// Connect application and Database (MySQL)
var connectionString = builder.Configuration.GetConnectionString("SalesWebAppContext");
builder.Services.AddDbContext<SalesWebAppContext>(options =>
options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), mysqlOptions =>
mysqlOptions.MigrationsAssembly("SalesWebApp")));

// Register the Seeding Service (to populate the database)
builder.Services.AddScoped<SeedingService>();

builder.Services.AddScoped<SellerService>();

builder.Services.AddScoped<SalesRecordService>();

builder.Services.AddScoped<DepartmentService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var seeder = services.GetRequiredService<SeedingService>();
    seeder.Seed();
}

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
