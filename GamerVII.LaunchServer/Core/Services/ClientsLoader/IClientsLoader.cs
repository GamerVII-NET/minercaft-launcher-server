using GamerVII.LauncherDomains.Models.Launcher;

namespace GamerVII.LaunchServer.Core.Services.ClientsLoader;

public interface IClientsLoader
{
    Task<bool> DownloadClientAsync(Client client);

}