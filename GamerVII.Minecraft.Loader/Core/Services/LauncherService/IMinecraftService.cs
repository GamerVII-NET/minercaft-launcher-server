using GamerVII.Minecraft.Loader.Models.Mojang;

namespace GamerVII.Minecraft.Loader.Core.Services.LauncherService;

public interface IMinecraftService
{
    Task<IEnumerable<MinercaftVersionManifest>> GetMinercaftVersionsVersions();
}