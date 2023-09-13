namespace GamerVII.Minecraft.Loader.Models.Client;

public class LocalSession : ISession
{
    public string UserName { get; set; }
    
    public LocalSession(string userName)
    {
        UserName = userName;
    }

}