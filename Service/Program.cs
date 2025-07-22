using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                host = host.UseWindowsService();
            }

            return host.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseUrls("http://*:8888");
                webBuilder.UseStartup<Startup>();
            });
        }
    }
}
