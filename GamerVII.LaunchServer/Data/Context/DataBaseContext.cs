using GamerVII.LauncherDomains.Models.Data.Models;
using GamerVII.LaunchServer.Core.Configs;
using GamerVII.LaunchServer.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace GamerVII.LaunchServer.Data.Context;

public class DataBaseContext : DbContext
{
    private readonly LaunchServerConfig _config;
    public DbSet<Launcher> LauncherVersions { get; set; }

    public DataBaseContext(LaunchServerConfig config, DbContextOptions<DataBaseContext> options) : base(options)
    {
        _config = config;

        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        switch (_config.DataStorageType)
        {
            case DataStorageType.Mysql:
                optionsBuilder.UseMySql(
                    $"server={_config.Connection.Host};user={_config.Connection.Login};password={_config.Connection.Password};database={_config.Connection.DataBaseName};", 
                    new MySqlServerVersion(new Version(_config.Connection.DataBaseVersion))
                );
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
    }
    
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Launcher>();

        base.OnModelCreating(builder);
    }
    
}