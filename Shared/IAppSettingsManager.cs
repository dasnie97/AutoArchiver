namespace AutoArchiver.Shared;

public interface IAppSettingsManager
{
    AppSettings GetAppSettings();
    AppSettings SaveAppSettings();
}