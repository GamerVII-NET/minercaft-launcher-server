using GamerVII.LauncherDomains.Models.Launcher;

namespace GamerVII.LauncherDomains.Models.Dto.GameClients;

public class ReadGameClientDto
{
    public string Name { get; set; }
    public ClientConfig Config { get; set; }
}