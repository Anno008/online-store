using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
              .UseKestrel()
              .UseContentRoot(Directory.GetCurrentDirectory())
              .UseUrls("http://*:8080")
              .UseStartup<Startup>()
              .Build();

            host.Run();
        }
    }
}
