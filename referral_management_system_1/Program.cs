using referral_management_system_1.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using referral_management_system_1;



namespace referral_management_system_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}