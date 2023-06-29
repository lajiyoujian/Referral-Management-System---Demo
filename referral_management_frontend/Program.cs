using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace referral_management_frontend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Inject HttpClient into Razor Pages
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var httpClient = services.GetRequiredService<System.Net.Http.HttpClient>();
                    httpClient.BaseAddress = new Uri("http://localhost:5045"); // Set your API base address here
                }
                catch (Exception ex)
                {
                    // Handle any errors that occurred while configuring HttpClient
                    Console.WriteLine("Error configuring HttpClient: " + ex.Message);
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}









//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddRazorPages();

//var app = builder.Build();


//app.UseHttpsRedirection();
//app.UseStaticFiles();
//app.UseRouting();
//app.UseAuthorization();

//app.MapRazorPages();

//await app.RunAsync();

//builder.Services.AddHttpClient("ReferralManagementAPI", client =>
//{
//    client.BaseAddress = new Uri("http://localhost:5045");
//     //  the base URL of your referral_management_system_1 API,
//    //  which is running from the launchSettings.json file in API project
//});