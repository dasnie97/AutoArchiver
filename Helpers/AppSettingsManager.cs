using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text.Json;

namespace AutoArchiver.Helpers;

public class AppSettingsManager : IAppSettingsManager
{
    private readonly IConfiguration _configuration;
    private AppSettings _appSettings;

    private string _appsettingsFilePath = "appsettings.json";

    public AppSettingsManager(IConfiguration configuration)
    {
        _configuration = configuration;
        _appSettings = new AppSettings();
        _configuration.Bind(_appSettings);
    }

    public AppSettings SaveAppSettings()
    {
        JsonSerializerOptions options = new JsonSerializerOptions();
        options.WriteIndented = true;

        string jsonString = JsonSerializer.Serialize(_appSettings, options);

        File.WriteAllText(_appsettingsFilePath, jsonString);
        return _appSettings;
    }

    public AppSettings GetAppSettings()
    {
        return _appSettings;
    }
}
