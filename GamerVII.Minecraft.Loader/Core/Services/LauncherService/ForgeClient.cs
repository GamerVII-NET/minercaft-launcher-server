using System.Diagnostics;
using GamerVII.Minecraft.Loader.Models.Client;

namespace GamerVII.Minecraft.Loader.Core.Services.LauncherService;

public class ForgeClient : IMinecraftClient
{
    public Task<bool> VerifyVersionAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Process> Launch(ISession session, LaunchSettings launchSettings, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}