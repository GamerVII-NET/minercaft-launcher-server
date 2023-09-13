namespace GamerVII.Minecraft.Loader.Models.Client;

public class LaunchSettings
{
    public string? ServerIp { get; set; }
    public int ServerPort { get; set; } = 25565;
    
    public int ScreenWidth { get; set; }
    public int ScreenHeight { get; set; }
    public bool FullScreen { get; set; }

    public bool EnableFileWatcher { get; set; }
}