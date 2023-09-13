using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GamerVII.Minecraft.Loader.Core.Configs;
using GamerVII.Minecraft.Loader.Core.Utils;
using GamerVII.Minecraft.Loader.Models.Client;
using GamerVII.Minecraft.Loader.Models.Mojang;
using Newtonsoft.Json;
using Version = GamerVII.Minecraft.Loader.Models.Mojang.Version;

namespace GamerVII.Minecraft.Loader.Core.Services.LauncherService
{
    public class MinecraftClient : IMinecraftClient
    {
        private MinercaftVersionManifest? _minecraftManifest;
        private IVersion? _clientVersion;
        private Version? _currentMinecraftVersion;
        private readonly string _minercaftVersion;
        private readonly MinecraftPath _gamePath;

        public MinecraftClient(string minercaftVersion, string clientName)
        {
            _minercaftVersion = minercaftVersion;
            _gamePath = new MinecraftPath(GamePathsConfig.GetOsDefaultPath(), clientName);
        }
        
        public MinecraftClient(string minercaftVersion, MinecraftPath gamePath)
        {
            _minercaftVersion = minercaftVersion;
            _gamePath = gamePath;
        }

        private bool CheckValidVersion()
        {
            _currentMinecraftVersion = _minecraftManifest?.Versions.FirstOrDefault(c => c.Id.Equals(_minercaftVersion));

            _clientVersion = new MinecraftVersion(_currentMinecraftVersion.Id);
            
            return _currentMinecraftVersion != null && _clientVersion != null;
        }

        private async Task LoadManifest(CancellationToken cancellationToken)
        {
            if (_minecraftManifest is null)
            {
                var content = await HttpGetRequester.MakeGetRequestAsync(MojangConfig.MojangServer.Versions, cancellationToken);

                _minecraftManifest = JsonConvert.DeserializeObject<MinercaftVersionManifest>(content);
            }
        }

        public async Task<bool> VerifyVersionAsync(CancellationToken cancellationToken = default)
        {
            await LoadManifest(cancellationToken);
            
            return CheckValidVersion();
        }

        public Task<Process> Launch(ISession session, LaunchSettings launchSettings, CancellationToken cancellationToken = default)
        {
            _gamePath.CheckOrCreateDirectory();

            return null;
        }
    }
}