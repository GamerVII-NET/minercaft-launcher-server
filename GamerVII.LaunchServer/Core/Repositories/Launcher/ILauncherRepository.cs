namespace GamerVII.LaunchServer.Core.Repositories.Launcher;

public interface ILauncherRepository
{

    Task<bool> CheckUpdate(LauncherDomains.Models.Data.Models.Launcher launcher);

    Task<LauncherDomains.Models.Data.Models.Launcher?> GetReleaseInfo();
    Task<LauncherDomains.Models.Data.Models.Launcher?> GetReleaseInfo(string version);
}