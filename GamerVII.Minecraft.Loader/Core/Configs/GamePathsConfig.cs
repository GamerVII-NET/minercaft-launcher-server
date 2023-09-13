using GamerVII.Minecraft.Loader.Core.Utils;

namespace GamerVII.Minecraft.Loader.Core.Configs;

public abstract class GamePathsConfig
{
    private static readonly string
        MacDefaultPath = Environment.GetEnvironmentVariable("HOME") ?? throw new InvalidOperationException(),
        LinuxDefaultPath = Environment.GetEnvironmentVariable("HOME") ?? throw new InvalidOperationException(),
        WindowsDefaultPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

    public static string GetOsDefaultPath()
    {
        switch (OperationSystemChecker.OSName)
        {
            case "osx":
                return MacDefaultPath;
            case "linux":
                return LinuxDefaultPath;
            case "windows":
                return WindowsDefaultPath;
            default:
                return Environment.CurrentDirectory;
        }
    }
}