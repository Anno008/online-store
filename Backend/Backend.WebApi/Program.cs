using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Backend.WebApi
{
    public class Program
    {
        public static void Main(string[] args) =>
            BuildWebHost(args).Run();

        public static IWebHost BuildWebHost(string[] args) =>
          WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}

// Consider figuring out either with conditional variables how to host this differently based
// on build config
//public static IWebHost BuildWebHost(string[] args) =>
//           new WebHostBuilder()
//             .UseKestrel()
//             .UseContentRoot(Directory.GetCurrentDirectory())
//             .UseUrls("http://*:8080")
//             .UseStartup<Startup>()
//             .Build();