namespace GamerVII.LauncherDomains.Models.Dto.GameClients;

public class CreateGameClientDto
{
    public required string Name { get; set; }
    public required string ClientVersion { get; set; }
}