using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Z.EntityFramework.Extensions;

namespace TestEFE
{
    internal static class Program
    {
        public static void BuildConfiguration(IConfigurationBuilder builder, string[]? args)
        {
            var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";

            builder.SetBasePath(GetBasePath())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                   .AddEnvironmentVariables();

            if (args != null)
            {
                builder.AddCommandLine(args, new Dictionary<string, string> { { "-i", "import" } });
            }
        }

        public static void ConfigureServices(HostBuilderContext hostCtx, IServiceCollection services)
        {
            var connStr = hostCtx.Configuration.GetConnectionString("GenericData");

            var options = new DbContextOptionsBuilder<Database.GenericDataContext>().UseSqlServer(connStr);

            services.AddDbContext<Database.GenericDataContext>(o => o.UseSqlServer(connStr));

            EntityFrameworkManager.ContextFactory = context => new Database.GenericDataContext(options.Options);

            services.AddHostedService<ImportsRunner>();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder()
                       .ConfigureAppConfiguration(b => BuildConfiguration(b, args))
                       .ConfigureServices(ConfigureServices);
        }

        public static async Task<int> Main(string[] args)
        {
            var result = 0;

            var builder = new ConfigurationBuilder();
            BuildConfiguration(builder, null);

            try
            {
                var host = CreateHostBuilder(args).Build();
                await host.RunAsync().ConfigureAwait(false);

                result = 0;
            }
            catch (Exception ex)
            {
                result = 1;
            }

            return result;
        }

        private static string GetBasePath()
        {
            using var processModule = Process.GetCurrentProcess().MainModule;
            return Path.GetDirectoryName(processModule?.FileName) ?? string.Empty;
        }
    }
}
