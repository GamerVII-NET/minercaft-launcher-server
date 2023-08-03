namespace GamerVII.LauncherDomains.Models.Dto.Server;

public class ServerAuthError
{
    public string error { get; set; }
    public string errorMessage { get; set; }
    public string cause { get; set; }
}