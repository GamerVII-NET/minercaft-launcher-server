using GamerVII.LaunchServer.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GamerVII.LaunchServer.Core.Repositories.Launcher;

public class LauncherRepository : ILauncherRepository
{
    private readonly DataBaseContext _context;

    public LauncherRepository(DataBaseContext context)
    {
        _context = context;
    }
    
    public Task<bool> CheckUpdate(LauncherDomains.Models.Data.Models.Launcher launcher)
    {
        return Task.FromResult(true);
    }

    public async Task<LauncherDomains.Models.Data.Models.Launcher?> GetReleaseInfo() => 
        await _context.LauncherVersions.OrderBy(c => c.CreatedAt).LastOrDefaultAsync();
    
    public async Task<LauncherDomains.Models.Data.Models.Launcher?> GetReleaseInfo(string version) => 
        await _context.LauncherVersions.LastOrDefaultAsync(c => c.Version.Equals(version));
}