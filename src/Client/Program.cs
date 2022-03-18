using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Client.Http;

namespace Client
{
  public static class Program
    {

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient<LocalClient>(
                client => {
                    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
                }
            );
            builder.Services.AddHttpClient<ServerApiClient>(
                client => {
                    client.BaseAddress = new Uri("https://services.exokomodo.com/api/");
                }
            );

            await builder.Build().RunAsync();
        }
    }
}
