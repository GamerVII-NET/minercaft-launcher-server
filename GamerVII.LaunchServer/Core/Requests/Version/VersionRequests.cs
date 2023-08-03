using GamerVII.LaunchServer.Core.Services.Clients;

namespace GamerVII.LaunchServer.Core.Requests.Version;

public abstract class VersionRequests
{
    
    public static async Task<IResult> GetVersions(IClientService clientService, HttpContext context)
    {
        var versions = await clientService.GetVersions();
        
        return Results.Ok(versions);
    }
}