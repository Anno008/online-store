using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Backend.WebApi
{
    public class Program
    {
        public static void Main(string[] args) =>
            BuildWebHost(args).Run();

        public static IWebHost BuildWebHost(string[] args) =>
            new WebHostBuilder()
              .UseKestrel()
              .UseContentRoot(Directory.GetCurrentDirectory())
              .UseUrls("http://*:8080")
              .UseStartup<Startup>()
              .Build();
    }
}
