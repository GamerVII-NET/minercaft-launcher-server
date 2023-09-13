using System.Diagnostics;
using GamerVII.Minecraft.Loader.Models.Client;

namespace GamerVII.Minecraft.Loader.Core.Services.LauncherService;

public interface IMinecraftClient
{
    Task<bool> VerifyVersionAsync(CancellationToken cancellationToken = default);
    Task<Process> Launch(ISession session, LaunchSettings launchSettings, CancellationToken cancellationToken = default);
}