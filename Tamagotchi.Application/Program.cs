using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Tamagotchi.Application.Application;
using Tamagotchi.Application.Infrastructure;
using Tamagotchi.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System;
using Autofac.Extensions.DependencyInjection;
using Tamagotchi.Application.Application.BackgroundTasks;

namespace Tamagotchi.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var host = CreateHostBuilder(args).Build();

                Log.Information("Applying migrations ({ApplicationContext})...", AppContext.AppName);
                host.MigrateDbContext<TamagotchiContext>((context, services) =>
                {
                    var env = services.GetService<IWebHostEnvironment>();
                    var logger = services.GetService<ILogger<TamagotchiContextSeed>>();

                    new TamagotchiContextSeed()
                        .SeedAsync(context, env, logger)
                        .Wait();
                });

                Log.Information("Starting web host ({ApplicationContext})...", AppContext.AppName);
                host.Run();
            }
            catch(Exception ex)
            {
                // Log.Logger will likely be internal type "Serilog.Core.Pipeline.SilentLogger".
                if (Log.Logger == null || Log.Logger.GetType().Name == "SilentLogger")
                {
                    Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .WriteTo.Console()
                        .CreateLogger();
                }

                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .CaptureStartupErrors(true)
                        .ConfigureAppConfiguration(config =>
                        {
                            config
                                // Used for local settings like connection strings.
                                .AddJsonFile("appsettings.json", optional: true)
                                .AddEnvironmentVariables();
                        })
                        .UseSerilog((hostingContext, loggerConfiguration) =>
                        {
                            var seqServerUrl = hostingContext.Configuration["SeqServerUrl"];

                            loggerConfiguration
                                .WriteTo.Seq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl)
                                .ReadFrom.Configuration(hostingContext.Configuration)
                                .Enrich.FromLogContext()
                                .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name)
                                .Enrich.WithProperty("Environment", hostingContext.HostingEnvironment);

#if DEBUG
                            // Used to filter out potentially bad data due debugging.
                            // Very useful when doing Seq dashboards and want to remove logs under debugging session.
                            loggerConfiguration.Enrich.WithProperty("DebuggerAttached", Debugger.IsAttached);
#endif
                        });
                })
                .ConfigureServices(services =>
                    services.AddHostedService<DragonGrowthBackgroundService>());
    }

    public static class AppContext
    {
        public static readonly string Namespace = typeof(Startup).Namespace;
        public static readonly string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
    }
}
