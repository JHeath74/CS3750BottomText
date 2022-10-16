using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BottomTextLMS
{

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
.ConfigureAppConfiguration((context, config) =>
{
<<<<<<< Updated upstream
//var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));
//config.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());
=======
    //var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));
    //config.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());
>>>>>>> Stashed changes
})
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

    }



}
