namespace GamerVII.Minecraft.Loader.Models.Client;

public class MinecraftVersion : IVersion
{
    public string Version { get; set; }
    
    public MinecraftVersion(string version)
    {
        Version = version;
    }

}