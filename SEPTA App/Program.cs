using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SEPTA_App.Data;
using SEPTA_App.Workers;
using Serilog;


Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, logConfig) => logConfig
.WriteTo.Console().ReadFrom.Configuration(context.Configuration));

// Add services to the container

string connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<SeptaDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<ISeptaAPIWorker, SeptaAPIWorker>();


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

