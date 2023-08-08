using GamerVII.LauncherDomains.Models.Data;
using GamerVII.LaunchServer.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace GamerVII.LaunchServer.Core.Configs;

public class LaunchServerConfig
{
    public string[] Mirrors { get; set; } = {
        "https://launcher.sashok724.net/download"
    };

    public DataStorageType DataStorageType = Enums.DataStorageType.Mysql;

    public readonly DataBaseConnection Connection = new()
    {
        Host = "localhost",
        Login = "root",
        Password = "",
        DataBaseVersion = "8.1.9",
        DataBaseName = "LauncherDevelop",
    };
    
}