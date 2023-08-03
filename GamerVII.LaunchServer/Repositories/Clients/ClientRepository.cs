using GamerVII.LauncherDomains.Models.Launcher;
using GamerVII.LaunchServer.Extensions.Client;
using GamerVII.LaunchServer.Services.System;

namespace GamerVII.LaunchServer.Repositories.Clients;

public class ClientRepository : IClientRepository
{
    private readonly IStorageService _storageService;

    public ClientRepository(IStorageService storageService)
    {
        _storageService = storageService;
    }

    public async Task<IEnumerable<Client>> GetClientsAsync()
    {
        List<Client> clients = new List<Client>();

        var clientsFolders = await _storageService.GetFolders();

        foreach (var folder in clientsFolders)
        {
            Client client = await CreateClient(folder.Name);

            clients.Add(client);
        }

        return clients.Where(c => c.Config != null);
    }

    private async Task<Client> CreateClient(string name)
    {
        var client = new Client
        {
            Name = name,
            ClientFolder = name
        };

        var config =
            await _storageService.ReadJsonFileAsync<ClientConfig>(
                $@"{_storageService.ClientDirectory}\{name}\{name}-config.json");

        if (config != null)
        {
            config.Command = CreateStartPath(client, config);

            client.Config = config;
        }

        return client;
    }

    private string CreateStartPath(Client client, ClientConfig clientConfig)
    {
        var arguments = CreateArguments(client, clientConfig);

        var command = string.Join(" ", arguments);

        // ToDo: Вынести в лаунчер
        Dictionary<string, string> dict = new Dictionary<string, string>
        {
            { "{javaPath}", @"C:\Program Files (x86)\Common Files\Oracle\Java\javapath\java.exe" },
            { "{localPath}", @"C:\Users\GamerVII\RiderProjects\GamerVII.LaunchServevr\GamerVII.LaunchServer" },
            { "{windowWidth}", "1200" },
            { "{windowHeight}", "1200" },
            { "{userName}", "GamerVII" },
            { "{uuid}", "31f5f47753db4afdb88d2e01815f4887" },
            { "{memorySize}", "4098" },
            { "{accessToken}", "feosfeuchnfw3g4nh9crhw34icrnhicuerhfnicw4i" },
        };

        foreach (var data in dict)
        {
            command = command.Replace(data.Key, data.Value);
        }

        return command;
    }

    public string[] CreateArguments(Client client, ClientConfig clientConfig)
    {
        List<string> arguments = new List<string>();

        arguments.Add(@"""{javaPath}""");
        arguments.Add("-XX:HeapDumpPath=ThisTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump");
        arguments.Add($@"-cp ""{string.Join(";", GetClassPaths(client))}""");
        arguments.Add(
            @"-D""java.library.path=" + $@"{{localPath}}\{client.GetGameClientNativesPath(_storageService)}""");
        arguments.Add(clientConfig.MainClass);

        arguments.Add($@"--gameDir {{localPath}}\{client.GetGameClientPath(_storageService)}");
        arguments.Add($@"--assetsDir {{localPath}}\{client.GetAssetsPath(_storageService)}");
        arguments.Add("--width {windowWidth}");
        arguments.Add("--height {windowHeight}");
        arguments.Add("--userType mojang");
        arguments.Add(
            "skinURL { http://textures.minecraft.net/texture/74d1e08b0bb7e9f590af27758125bbed1778ac6cef729aedfcb9613e9911ae75 }");
        arguments.Add(
            "cloakURL http://textures.minecraft.net/texture/74d1e08b0bb7e9f590af27758125bbed1778ac6cef729aedfcb9613e9911ae75");
        arguments.Add($"--version {clientConfig.Version}");
        arguments.Add($"--assetIndex {clientConfig.AssetIndex}");

        arguments.Add("--username {userName}");
        arguments.Add("--uuid {uuid}");
        arguments.Add("--accessToken {accessToken}");
        arguments.Add("--userProperties {}");
        arguments.Add("-Xms {memorySize}M");
        arguments.Add("-Xmx {memorySize}");

        arguments.Add(string.Join(" ", clientConfig.JvmArguments));
        arguments.Add(string.Join(" ", clientConfig.ClientArguments));

        return arguments.ToArray();
    }

    private IEnumerable<string> GetClassPaths(Client client)
    {
        List<string> libs = new List<string>();

        var clientFolder = new DirectoryInfo(client.GetGameClientPath(_storageService));
        var librariesFolder = new DirectoryInfo(client.GetGameClientLibrariesPath(_storageService));

        if (clientFolder.Exists)
        {
            libs = librariesFolder.GetFiles("*.*", SearchOption.AllDirectories)
                .Select(c =>
                    $@"{{localPath}}\Launcher\Clients\{client.Name}\Client\{c.FullName.Split(@"Client\").Last()}")
                .ToList();
        }

        if (clientFolder.Exists)
        {
            var mainClasses = clientFolder.GetFiles("*.jar", SearchOption.TopDirectoryOnly)
                .Select(c =>
                    $@"{{localPath}}\Launcher\Clients\{client.Name}\Client\{c.FullName.Split(@"Client\").Last()}")
                .ToList();

            libs.AddRange(mainClasses);
        }

        return libs.ToArray();
    }

    public async Task<Client?> GetClientByNameAsync(string name)
    {
        DirectoryInfo clientDirectory = await _storageService.GetClientFolder(name);

        if (!clientDirectory.Exists)
        {
            return null;
        }

        return await CreateClient(name);
    }
}