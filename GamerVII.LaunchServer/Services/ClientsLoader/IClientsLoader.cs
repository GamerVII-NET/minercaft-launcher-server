using GamerVII.LauncherDomains.Models.Launcher;

namespace GamerVII.LaunchServer.Services.ClientsLoader;

public interface IClientsLoader
{
    public string[] Mirrors { get; set; }

    Task<bool> DownloadClientAsync(Client client);

}