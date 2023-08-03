using GamerVII.LaunchServer.Core.Services.System;

namespace GamerVII.LaunchServer.Core.Extensions.Client;

public static class ClientExtensions
{

    public static string GetGameClientPath(this LauncherDomains.Models.Launcher.Client client, IStorageService storageService)
    {
        return $@"{storageService.ClientDirectory}\{client.Name}\Client";
    }
    
    public static string GetGameClientConfigPath(this LauncherDomains.Models.Launcher.Client client, IStorageService storageService)
    {
        return $@"{GetGameClientPath(client, storageService)}\Config";
    }
    
    public static string GetGameClientLibrariesPath(this LauncherDomains.Models.Launcher.Client client, IStorageService storageService)
    {
        return $@"{GetGameClientPath(client, storageService)}\Libraries";
    }
    
    public static string GetGameClientModsPath(this LauncherDomains.Models.Launcher.Client client, IStorageService storageService)
    {
        return $@"{GetGameClientPath(client, storageService)}\Mods";
    }
    
    public static string GetGameClientNativesPath(this LauncherDomains.Models.Launcher.Client client, IStorageService storageService)
    {
        return $@"{storageService.ClientDirectory}\{client.Name}\Client\natives";
    }
    
    public static string GetAssetsPath(this LauncherDomains.Models.Launcher.Client client, IStorageService storageService)
    {
        return $@"{storageService.ClientDirectory}\{client.Name}\Assets";
    }
    
}