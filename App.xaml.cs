using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using Serilog;

namespace AutoArchiver
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            HostApplicationBuilder builder = CreateApplicationHostBuilder();
            IHost host = builder.Build();
            host.Run();
        }

        private static HostApplicationBuilder CreateApplicationHostBuilder()
        {
            var builder = Host.CreateApplicationBuilder();
            builder.Configuration.Sources.Clear();
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            builder.Services.AddTransient<IHostedService, MainWindow>();
            builder.Services.AddSerilog(config =>
            {
                config.ReadFrom.Configuration(builder.Configuration);
                config.Enrich.FromLogContext();
                config.WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day);
            });

            return builder;
        }
    }
}
