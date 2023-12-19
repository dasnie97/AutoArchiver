namespace AutoArchiver.Helpers;

public interface IAppSettingsManager
{
    AppSettings GetAppSettings();
    AppSettings SaveAppSettings();
}