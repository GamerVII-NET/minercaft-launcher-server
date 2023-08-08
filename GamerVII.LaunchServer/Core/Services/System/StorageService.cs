using Newtonsoft.Json;

namespace GamerVII.LaunchServer.Core.Services.System;

public class StorageService : IStorageService
{
    public string BaseDirectory { get; set; } = @"Launcher";
    public string ClientDirectory { get; set; } = $@"Launcher\Clients";

    public Task<string> CreateDirectoryAsync(string relativePath)
    {
        return Task.FromResult(Directory.CreateDirectory(relativePath).FullName);
    }

    public Task<bool> CheckDirectoryExistsAsync(string relativePath)
    {
        return Task.FromResult(!new DirectoryInfo(relativePath).Exists);
    }

    public async Task<T?> CreateJsonFileAsync<T>(string fileName, string template)
    {
        await using var fileWriter = new StreamWriter(fileName);
        await fileWriter.WriteAsync(template);

        return JsonConvert.DeserializeObject<T>(template);

    }

    public async Task<T?> ReadJsonFileAsync<T>(string relativePath)
    {
        var content = await ReadFile(relativePath);

        return content == null ? default : JsonConvert.DeserializeObject<T>(content);
    }

    public Task<bool> DeleteFolder(string relativePath)
    {
        var isSuccess = true;
        
        try
        {
            new DirectoryInfo(relativePath).Delete(true);
        }
        catch (Exception)
        {
            isSuccess = false;
        }

        return Task.FromResult(isSuccess);
    }

    public async Task<string?> ReadFile(string relativePath)
    {
        if (!File.Exists(relativePath))
        {
            return null;
        }
        
        return await File.ReadAllTextAsync(relativePath);
    }

    public Task<IEnumerable<DirectoryInfo>> GetFolders()
    {
        var baseDirectory = new DirectoryInfo(ClientDirectory);

        if (!baseDirectory.Exists)
        {
            baseDirectory.Create();
        }

        var directories = baseDirectory.GetDirectories().AsEnumerable();
        
        return Task.FromResult(directories);
    }

    public Task<DirectoryInfo> GetClientFolder(string name)
    {
        return Task.FromResult(new DirectoryInfo($@"{ClientDirectory}\{name}"));
    }
    
    public Task<DirectoryInfo> GetFolder(string name)
    {
        return Task.FromResult(new DirectoryInfo($@"{BaseDirectory}\{name}"));
    }
}