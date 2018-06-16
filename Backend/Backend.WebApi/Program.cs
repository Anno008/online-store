using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Backend.WebApi
{
    public static class Program
    {
        public static void Main(string[] args) =>
            BuildWebHost(args).Run();

        public static IWebHost BuildWebHost(string[] args)
        {
#if DEBUG
            return new WebHostBuilder()
                     .UseKestrel()
                     .UseContentRoot(Directory.GetCurrentDirectory())
                     .UseUrls("http://*:8080")
                     .UseStartup<Startup>()
               .Build();
#else
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
#endif
        }
    }
}
