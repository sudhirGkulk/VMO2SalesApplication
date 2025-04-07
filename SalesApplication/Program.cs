using SalesApplication.Web.Services.Service;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Register SalesService
        builder.Services.AddSingleton<SalesService>();

        builder.Services.AddControllersWithViews();
        var app = builder.Build();

        app.MapDefaultControllerRoute();
        app.Run();
    }
}