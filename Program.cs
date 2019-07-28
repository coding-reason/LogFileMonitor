using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading;
namespace LogFileMonitor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var tid = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"Main: {tid}");
            CreateWebHostBuilder(args).Build().Run();
            
        }
        public static readonly object fileLock = new object();
        
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
