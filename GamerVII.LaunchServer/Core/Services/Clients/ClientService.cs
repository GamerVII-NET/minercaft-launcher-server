using System.Text.RegularExpressions;
using GamerVII.LauncherDomains.Models.Dto.GameClients;
using GamerVII.LauncherDomains.Models.Launcher;
using GamerVII.LaunchServer.Core.Repositories.Clients;
using GamerVII.LaunchServer.Core.Services.ClientsLoader;
using GamerVII.LaunchServer.Core.Services.System;
using GamerVII.LaunchServer.Core.Services.System.StorageService;

namespace GamerVII.LaunchServer.Core.Services.Clients;

public partial class ClientService : IClientService
{
    private readonly IStorageService _storageService;
    private readonly IClientRepository _clientRepository;
    private readonly IClientsLoader _clientsLoader;

    private readonly Regex _regex = GenerateVersionRegex();

    public ClientService(IStorageService storageService,
        IClientRepository clientRepository,
        IClientsLoader clientsLoader)
    {
        _storageService = storageService;
        _clientRepository = clientRepository;
        _clientsLoader = clientsLoader;
    }

    public async Task<IEnumerable<ClientVersion>> GetVersions()
    {
        var folder = await _storageService.GetFolder("ClientConfigs\\Templates");

        if (folder.Exists)
        {
            var fileVersions = folder.GetFiles("client-template-*.json");

            return fileVersions.Where(c => _regex.IsMatch(c.Name))
                .Select(c => new ClientVersion { Version = _regex.Match(c.Name).Groups[1].Value });
        }


        return Enumerable.Empty<ClientVersion>();
    }

    public async Task<Client> CreateClient(CreateGameClientDto client)
    {
        var gameClient = new Client
        {
            Name = client.Name,
        };

        gameClient.ClientFolder =
            await _storageService.CreateDirectoryAsync($@"{_storageService.ClientDirectory}\{client.Name}");
        gameClient.Config =
            await _storageService.CreateJsonFileAsync<ClientConfig>($@"{gameClient.ClientFolder}\{client.Name}-config.json",
                await GenerateConfig(client));

        return gameClient;
    }

    private async Task<string> GenerateConfig(CreateGameClientDto client)
    {
        var configTemplatesFolder = await _storageService.GetFolder("ClientConfigs\\Templates");
        
        string configFile = configTemplatesFolder
            .GetFiles($"client-template-{client.ClientVersion}.json")
            .SingleOrDefault()!.FullName;

        string template = await _storageService.ReadFile(configFile) ?? throw new InvalidOperationException();

        var replaceParameters = new Dictionary<string, string>
        {
            { "{version}", client.ClientVersion },
            { "{assetIndex}", client.ClientVersion },
            { "{name}", client.Name }
        };

        foreach (var parameter in replaceParameters)
        {
            template = template.Replace(parameter.Key, parameter.Value);
        }

        return template;
    }

    public async Task<bool> Check(CreateGameClientDto client)
    {
        var allowCreateDirectory =
            await _storageService.CheckDirectoryExistsAsync($@"{_storageService.ClientDirectory}\{client.Name}");

        var allowCreateVersion = (await GetVersions()).FirstOrDefault(c => c.Version == client.ClientVersion) != null;

        return allowCreateDirectory && allowCreateVersion;
    }

    public Task<bool> DeleteClient(RemoveGameClientDto client)
    {
        return _storageService.DeleteFolder($@"{_storageService.ClientDirectory}\{client.Name}");
    }

    public Task<IEnumerable<Client>> GetClients()
    {
        return _clientRepository.GetClientsAsync();
    }

    public Task<Client?> GetClientByName(string name)
    {
        return _clientRepository.GetClientByNameAsync(name);
    }

    public async Task<bool> LoadClient(Client client)
    {
        var isDownloaded = await _clientsLoader.DownloadClientAsync(client);

        return isDownloaded;
    }

    [GeneratedRegex("client-template-([0-9]+(\\.[0-9]+)+)\\.json", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex GenerateVersionRegex();
}