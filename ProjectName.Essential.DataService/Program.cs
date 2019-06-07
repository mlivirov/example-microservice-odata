using System.Reflection;
using Crisp.Extensions.Configuration.Zookeeper;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ProjectName.Essential.DataService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.AddZookeeper(option =>
                    {
                        option.ConnectionString = "localhost:2181";
                        option.RootPath = typeof(Program).Namespace;
                    });
                })
                .UseStartup<Startup>();
    }
}