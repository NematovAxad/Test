using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace UserApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "User Api";
            Console.WriteLine($@"Process Id: {Process.GetCurrentProcess().Id}");

            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
              WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://*:6002")
                .UseStartup<Startup>();
    }
}
