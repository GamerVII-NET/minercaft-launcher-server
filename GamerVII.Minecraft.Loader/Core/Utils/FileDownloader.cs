using GamerVII.Minecraft.Loader.Models;

namespace GamerVII.Minecraft.Loader.Core.Utils
{
    internal class FileDownloader
    {
        private HttpClient _httpClient = new();
        public event Action<DownloadProgress> DownloadProgressChanged;

        public async Task<DownloadProgress> DownloadFileAsync(string url, string outputPath)
        {
            if (await InternetChecker.IsInternetAvailableAsync() == false)
                throw new InvalidOperationException("No internet connection.");
            
            using var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            using var content = response.Content;
            var totalSize = content.Headers.ContentLength ?? -1;
            var bytesRead = 0L;

            await using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 8192, useAsync: true))
            await using (var stream = await content.ReadAsStreamAsync())
            {
                var buffer = new byte[8192];
                var isMoreToRead = true;

                do
                {
                    var bytesReadThisTime = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesReadThisTime == 0)
                    {
                        isMoreToRead = false;
                        UpdateProgress(1.0);
                        break;
                    }

                    bytesRead += bytesReadThisTime;
                    await fileStream.WriteAsync(buffer, 0, bytesReadThisTime);

                    var progress = (double)bytesRead / totalSize;
                    UpdateProgress(progress);

                } while (isMoreToRead);
            }

            return new DownloadProgress
            {
                TotalSize = totalSize,
                Percentage = 1.0,
                Fraction = 1.0
            };
        }

        private void UpdateProgress(double progress)
        {
            DownloadProgressChanged?.Invoke(new DownloadProgress
            {
                Percentage = progress,
                Fraction = progress
            });
        }
    }
}
