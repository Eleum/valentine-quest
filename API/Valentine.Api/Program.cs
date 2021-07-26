using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Valentine.Api
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
                    webBuilder.ConfigureAppConfiguration((context, config) =>
                    {
                        var settings = config.Build();
                        config.AddAzureAppConfiguration(options =>
                        {
                            options.Connect(!context.HostingEnvironment.IsDevelopment() 
                                ? settings["Azure:AppSettingsEndpoint"] 
                                : settings.GetConnectionString("AzureAppSettingsConnectionString"))
                                .ConfigureKeyVault(kv =>
                                {
                                    kv.SetCredential(!context.HostingEnvironment.IsDevelopment() 
                                        ? new DefaultAzureCredential() 
                                        : new ClientSecretCredential(
                                            settings["ValentineTenantId"],
                                            settings["ValentineClientId"],
                                            settings["ValentineClientSecret"])
                                        );
                                });
                        });
                    }).UseStartup<Startup>();
                });
    }
}
