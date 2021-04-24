using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace WebStore
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                // ���� �� �������� ���������������� ������� ������������
                //.ConfigureLogging((host, log) => log
                //    .ClearProviders()
                //    .AddEventLog()
                //    .AddConsole()
                //    .AddFilter<ConsoleLoggerProvider>("Mictosoft.Hosting", LogLevel.Error)
                //    .AddFilter((category, level) => !(category.StartsWith("Microsoft") && level >= LogLevel.Warning))
                //)
                .ConfigureWebHostDefaults(host => host
                   .UseStartup<Startup>()
                )
            ;
    }
}
