using SalesApplication.Repository.Implementation;
using SalesApplication.Services.Services.Service;
using SalesApplication.Repository.Interface;
using SalesApplication.Services.Services.Interfaces;
using SalesApplication.Services.Services.Middlewares;
using Serilog;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "bin", "logs", "app.txt");


        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()

            .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
            .CreateLogger();
        builder.Host.UseSerilog();


        // Register SalesService
        builder.Services.AddSingleton<SalesService>();
        builder.Services.AddSingleton<SalesDataRepository>();

        builder.Services.AddTransient<ISalesDataRepository, SalesDataRepository>();
        builder.Services.AddTransient<ISalesService, SalesService>();




        // Add Cors Orgin Policy
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });



        builder.Services.AddControllersWithViews();
        var app = builder.Build();

        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseAuthorization();

        app.UseCors("AllowAll");
        app.MapDefaultControllerRoute();
        app.UseMiddleware<RequestResponseLoggingMiddleware>();
        app.MapFallbackToFile("/index.html");
        app.Run();
    }
}