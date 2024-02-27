using AutoArchiver;
using AutoArchiver.Shared;
using Serilog;

HostApplicationBuilder builder = CreateApplicationHostBuilder();
IHost host = builder.Build();
host.Run();


static HostApplicationBuilder CreateApplicationHostBuilder()
{
    var builder = Host.CreateApplicationBuilder();
    builder.Configuration.Sources.Clear();
    builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    builder.Services.AddHostedService<Worker>();
    builder.Services.AddTransient<IAppSettingsManager, AppSettingsManager>();
    builder.Services.AddSerilog(config =>
    {
        config.ReadFrom.Configuration(builder.Configuration);
        config.Enrich.FromLogContext();
        config.WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day);
    });

    return builder;
}