namespace GamerVII.LauncherDomains.Models.Launcher;

public class ClientConfig
{
    public string Name { get; set; }
    public string Version { get; set; }
    public string AssetIndex { get; set; }
    public string AssetDirectory { get; set; }
    public string ClientFolderName { get; set; }
    public string MainClass { get; set; }
    public string Command { get; set; }
    public List<string> UpdateVerify { get; set; }
    public List<string> ClassPath { get; set; }
    public List<string> JvmArguments { get; set; }
    public List<string> ClientArguments { get; set; }
}