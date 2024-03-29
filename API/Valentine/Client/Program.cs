using BlazorStrap;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Valentine.Application.Interfaces;
using Valentine.Application.Services;
using Valentine.Client.Helpers;
using Valentine.Client.States;

namespace Valentine.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient(Constants.Api.INTERNAL_API, client => 
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
            builder.Services.AddHttpClient(Constants.Api.WEB_API, client => 
                client.BaseAddress = new Uri("https://localhost:44399/"));

            builder.Services.AddScoped<ILayoutService, LayoutService>();
            builder.Services.AddScoped(typeof(MapInteractionState));

            await builder.Build().RunAsync();
        }
    }
}
