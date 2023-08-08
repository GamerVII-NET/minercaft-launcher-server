using System.IO.Compression;
using System.Net;
using GamerVII.LauncherDomains.Models.Launcher;
using GamerVII.LaunchServer.Core.Configs;
using GamerVII.LaunchServer.Core.Services.System;
using GamerVII.LaunchServer.Core.Services.System.StorageService;

namespace GamerVII.LaunchServer.Core.Services.ClientsLoader;

public class ClientsLoader : IClientsLoader
{
    private readonly IStorageService _storageService;
    
    private readonly LaunchServerConfig _config;

    public ClientsLoader(IStorageService storageService, LaunchServerConfig config)
    {
        _storageService = storageService;
        _config = config;
    }

    public async Task<bool> DownloadClientAsync(Client client)
    {
        bool isDownloaded = false;
        string? mirror = await CheckClientFromServer(client);
        string? mirrorAsset = await CheckAssetsFromServer(client);

        if (!string.IsNullOrEmpty(mirror) && !string.IsNullOrEmpty(mirrorAsset))
        {
            var downloadedClient = await DownloadFile(mirror, $@"{_storageService.ClientDirectory}\{client.ClientFolder}\Client");
            var downloadedAssets = await DownloadFile(mirrorAsset, $@"{_storageService.ClientDirectory}\{client.ClientFolder}\Assets");

            isDownloaded = downloadedClient is { Exists: true } && downloadedAssets is { Exists: true };

            if (isDownloaded)
            {
                await ExtractArchiveAsync(downloadedClient!.FullName, downloadedClient!.DirectoryName!);
                await ExtractArchiveAsync(downloadedAssets!.FullName, downloadedAssets!.DirectoryName!);
            }
        }

        return isDownloaded;
    }

    private async Task<string?> CheckAssetsFromServer(Client client)
    {
        try
        {
            foreach (var mirror in _config.Mirrors)
            {
                var url = $"{mirror}/assets/{client.Config?.Version}.zip";
                using var httpClient = new HttpClient();
                using var request = new HttpRequestMessage(HttpMethod.Head, url);
                var response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return url;
                }
            }
        }
        catch
        {
            // ignored
        }

        return null;
    }

    private static async Task<FileInfo?> DownloadFile(string url, string path)
    {
        try
        {
            var directory = new DirectoryInfo(path);
            var downloadingFile = new FileInfo($"{path}/archive.zip");

            if (downloadingFile.Exists)
            {
                return downloadingFile;
            }

            if (!directory.Exists)
            {
                directory.Create();
            }

            using HttpClient httpClient = new HttpClient();
            var fileBytes = await httpClient.GetByteArrayAsync(url);

            await File.WriteAllBytesAsync(downloadingFile.FullName, fileBytes);

            return new FileInfo($"{path}/archive.zip");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Произошла ошибка при выполнении HTTP-запроса: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }

        return null;
    }

    private async Task<string?> CheckClientFromServer(Client client)
    {
        try
        {
            foreach (var mirror in _config.Mirrors)
            {
                var url = $"{mirror}/clients/{client.Config?.Version}.zip";
                using var httpClient = new HttpClient();
                using var request = new HttpRequestMessage(HttpMethod.Head, url);
                var response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return url;
                }
            }
        }
        catch
        {
            // ignored
        }

        return null;
    }

    private static async Task ExtractArchiveAsync(string archivePath, string extractPath)
    {
        var pathSeparator = Path.DirectorySeparatorChar;

        Directory.CreateDirectory(extractPath);

        await using (var archiveStream = new FileStream(archivePath, FileMode.Open))
        {
            using var archive = new ZipArchive(archiveStream);
            if (archivePath.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var entry in archive.Entries)
                {
                    var destinationPath = Path.Combine(extractPath, entry.FullName.Replace('/', pathSeparator));

                    Directory.CreateDirectory(Path.GetDirectoryName(destinationPath) ??
                                              throw new InvalidOperationException());

                    if (entry.Length == 0)
                    {
                        Directory.CreateDirectory(destinationPath);
                    }
                    else
                    {
                        await using var entryStream = entry.Open();
                        await using var fileStream = new FileStream(destinationPath, FileMode.Create);
                        await entryStream.CopyToAsync(fileStream);
                    }
                }
            }
            else
            {
                throw new NotSupportedException("Неподдерживаемый формат архива.");
            }
        }

        File.Delete(archivePath);
    }
}