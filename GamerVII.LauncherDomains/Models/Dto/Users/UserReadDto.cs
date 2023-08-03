namespace GamerVII.LauncherDomains.Models.Dto.Users;

public class UserReadDto
{
    public required string Login { get; set; }
    public string AccessToken { get; set; }
    public string UUID { get; set; }
}