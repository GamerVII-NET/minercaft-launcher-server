namespace GamerVII.Minecraft.Loader.Models.Client;

public class MinecraftPath
{
    public string ClientName { get; }
    public string GamePath { get; set; }
    
    public MinecraftPath(string uri, string clientName)
    {
        ClientName = clientName;
        GamePath = Path.GetFullPath(uri);
    }


    public void CheckOrCreateDirectory()
    {
        var path = Path.Combine(GamePath, ClientName);
        
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}