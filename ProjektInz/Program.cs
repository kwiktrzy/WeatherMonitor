using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ProjektInz
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureLogging(logging=>
            {
                logging.ClearProviders();
                logging.AddConsole();
            })//TODO : change it to 192.168.43.27:44366
                .UseUrls("http://192.168.1.27:8899;https://192.168.1.27:44366")
                .UseKestrel()
                .UseStartup<Startup>();
    }
}
