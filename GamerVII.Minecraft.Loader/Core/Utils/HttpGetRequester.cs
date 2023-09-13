namespace GamerVII.Minecraft.Loader.Core.Utils
{
    internal abstract class HttpGetRequester
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public static async Task<string> MakeGetRequestAsync(string url, CancellationToken cancellationToken = default)
        {
            if (!await InternetChecker.IsInternetAvailableAsync())
            {
                throw new InvalidOperationException("No internet connection.");
            }

            var response = await _httpClient.GetAsync(url, cancellationToken);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync(cancellationToken);
        }
    }
}