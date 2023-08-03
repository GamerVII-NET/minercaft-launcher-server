using GamerVII.LauncherDomains.Models.Dto.GameClients;
using GamerVII.LauncherDomains.Models.Launcher;

namespace GamerVII.LaunchServer.Services.Clients;

public interface IClientService
{

    Task<IEnumerable<ClientVersion>> GetVersions();
    Task<Client> CreateClient(CreateGameClientDto client);
    Task<bool> Check(CreateGameClientDto client);
    Task<bool> DeleteClient(RemoveGameClientDto client);
    Task<IEnumerable<Client>> GetClients();
    Task<Client?> GetClientByName(string name);
    Task<bool> LoadClient(Client client);
}