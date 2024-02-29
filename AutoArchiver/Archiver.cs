using AutoArchiver.Shared;

namespace AutoArchiver;

public interface IArchiver
{
    Task<string> Archive();
}

public class Archiver : IArchiver
{
    public IAppSettingsManager _appSettingsManager { get; }

    public Archiver(IAppSettingsManager appSettingsManager)
    {
        _appSettingsManager = appSettingsManager;
    }

    public Task<string> Archive()
    {
        return Task.Run(() => { return "Im a running code!"; });
    }
}
