using GamerVII.LauncherDomains.Models.Launcher;

namespace GamerVII.LaunchServer.Repositories.Clients;

public interface IClientRepository
{
    Task<IEnumerable<Client>> GetClientsAsync();
    Task<Client?> GetClientByNameAsync(string name);
}