using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace MuseServer // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
    }
}