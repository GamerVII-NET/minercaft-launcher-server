namespace GamerVII.LauncherDomains.Models.Dto.Server;

public class ServerHasJoinedDto
{
    public string id { get; set; }
    public string name { get; set; }
    public List<Property> properties { get; set; }
    public ProfileActions profileActions { get; set; }
}

public class ProfileActions
{
}

public class Property
{
    public string name { get; set; }
    public string value { get; set; }
}