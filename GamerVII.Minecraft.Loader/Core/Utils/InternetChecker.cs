using System.Net.NetworkInformation;

namespace GamerVII.Minecraft.Loader.Core.Utils;

internal class InternetChecker
{
    internal static async Task<bool> IsInternetAvailableAsync()
    {
        try
        {
            using var ping = new Ping();
            var reply = await ping.SendPingAsync("8.8.8.8");
            return reply.Status == IPStatus.Success;
        }
        catch (Exception)
        {
            return false;
        }
    }
}