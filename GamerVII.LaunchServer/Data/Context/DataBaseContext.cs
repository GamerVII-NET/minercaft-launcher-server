using GamerVII.LaunchServer.Core.Configs;
using Microsoft.EntityFrameworkCore;

namespace GamerVII.LaunchServer.Data.Context;

public class DataBaseContext: DbContext
{
    private readonly LaunchServerConfig _config;

    public DataBaseContext(LaunchServerConfig config = null!)
    {
        _config = config;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql($"server={_config.Connection.Host};user={_config.Connection.User};password={_config.Connection.Password};database={_config.Connection.Host};", 
            new MySqlServerVersion(new Version(8, 0, 25)));
    }
    
}