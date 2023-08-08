using GamerVII.LauncherDomains.Models.Data;
using GamerVII.LaunchServer.Core.Enums;

namespace GamerVII.LaunchServer.Core.Configs;

public class LaunchServerConfig
{
    public string[] Mirrors { get; set; } = {
        "https://launcher.sashok724.net/download"
    };

    public readonly DataStorageType DataStorageType = DataStorageType.Mysql;

    public readonly DataBaseConnection Connection = new DataBaseConnection
    {
        Host = "localhost",
        Login = "root",
        Password = "",
        User = "root",
        DataBaseName = ""
    };

}