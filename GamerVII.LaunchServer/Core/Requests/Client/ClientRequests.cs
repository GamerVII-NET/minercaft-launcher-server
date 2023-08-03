using AutoMapper;
using GamerVII.LauncherDomains.Models.Dto.GameClients;
using GamerVII.LaunchServer.Core.Services.Clients;
using Microsoft.AspNetCore.Mvc;

namespace GamerVII.LaunchServer.Core.Requests.Client;

public static class ClientRequests
{
    public static async Task<IResult> CreateClient(IClientService clientService, IMapper mapper, CreateGameClientDto client)
    {
        bool allowCreateClient = await clientService.Check(client);

        if (!allowCreateClient)
        {
            return Results.BadRequest();
        }

        LauncherDomains.Models.Launcher.Client gameClient = await clientService.CreateClient(client);
        
        return Results.Ok(mapper.Map<ReadGameClientDto>(gameClient));
    }
    
    public static async Task<IResult> RemoveClient(IClientService clientService, [FromBody] RemoveGameClientDto client)
    {
        
        var isSuccess = await clientService.DeleteClient(client);
        
        return Results.Ok(isSuccess);
    }
    
    public static async Task<IResult> GetClients(IClientService clientService, IMapper mapper)
    {
        var clients = await clientService.GetClients();
        
        return Results.Ok(mapper.Map<IEnumerable<ReadGameClientDto>>(clients));
    }
    
    public static async Task<IResult> GetClientByName(IClientService clientService, IMapper mapper, string name)
    {
        LauncherDomains.Models.Launcher.Client? client = await clientService.GetClientByName(name);

        if (client == null)
        {
            return Results.NotFound();
        }
        
        return Results.Ok(mapper.Map<ReadGameClientDto>(client));
    }
    
    public static async Task<IResult> LoadClient(IClientService clientService, string name)
    {
        LauncherDomains.Models.Launcher.Client? client = await clientService.GetClientByName(name);

        if (client == null)
        {
            return Results.NotFound();
        }
        
        var isLoaded = await clientService.LoadClient(client);

        return Results.Ok(isLoaded);
    }
}