namespace GamerVII.LauncherDomains.Models.Launcher;

public class Client
{
    public string Name { get; set; }
    public string ClientFolder { get; set; }
    public ClientConfig? Config { get; set; }
}