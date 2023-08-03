namespace GamerVII.LauncherDomains.Models.Dto.Server;

public class TextureProperties
{
    public long timestamp { get; set; }
    public string profileId { get; set; }
    public string profileName { get; set; }
    public Textures textures { get; set; }
    
}
public class CAPE
{
    public string url { get; set; }
}

public class SKIN
{
    public string url { get; set; }
}

public class Textures
{
    public SKIN SKIN { get; set; }
    public CAPE CAPE { get; set; }
}