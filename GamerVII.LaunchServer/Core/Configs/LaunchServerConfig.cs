using GamerVII.LauncherDomains.Models.Data;
using GamerVII.LaunchServer.Core.Enums;

namespace GamerVII.LaunchServer.Core.Configs;

public class LaunchServerConfig
{
    public string[] Mirrors { get; set; } = {
        "https://launcher.sashok724.net/download"
    };

    public DataStorageType DataStorageType = DataStorageType.Mysql;

    public DataBaseConnection Connection = new DataBaseConnection
    {
        Host = "localhost",
        Login = "root",
        Password = "",
        User = "root",
        DataBaseName = ""
    };

}