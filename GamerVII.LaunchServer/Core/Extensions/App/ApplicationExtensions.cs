using GamerVII.LaunchServer.Core.Configs;
using GamerVII.LaunchServer.Core.Enums;
using GamerVII.LaunchServer.Core.Profiles.ClientProfiles;
using GamerVII.LaunchServer.Core.Profiles.LauncherProfiles;
using GamerVII.LaunchServer.Core.Repositories.Clients;
using GamerVII.LaunchServer.Core.Repositories.Launcher;
using GamerVII.LaunchServer.Core.Requests;
using GamerVII.LaunchServer.Core.Requests.Auth;
using GamerVII.LaunchServer.Core.Requests.Client;
using GamerVII.LaunchServer.Core.Requests.Launcher;
using GamerVII.LaunchServer.Core.Requests.Version;
using GamerVII.LaunchServer.Core.Services.Auth;
using GamerVII.LaunchServer.Core.Services.Clients;
using GamerVII.LaunchServer.Core.Services.ClientsLoader;
using GamerVII.LaunchServer.Core.Services.System;
using GamerVII.LaunchServer.Core.Services.System.StorageService;
using GamerVII.LaunchServer.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GamerVII.LaunchServer.Core.Extensions.App;

public static class ApplicationExtensions
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IClientsLoader, ClientsLoader>();
        builder.Services.AddSingleton<IClientRepository, ClientRepository>();
        builder.Services.AddSingleton<IStorageService, StorageService>();
        builder.Services.AddSingleton<IClientService, ClientService>();
        builder.Services.AddSingleton<IAuthService, DataBaseAuthService>();
        builder.Services.AddTransient<ILauncherRepository, LauncherRepository>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }

    public static WebApplicationBuilder AddDatabaseContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<LaunchServerConfig>();
        builder.Services.AddDbContext<DataBaseContext>();

        return builder;
    }

    public static WebApplicationBuilder AddAutoMapper(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(ClientProfiles));
        builder.Services.AddAutoMapper(typeof(LauncherProfiles));

        return builder;
    }

    public static WebApplication AddRoutes(this WebApplication app)
    {
        app.MapGet("/", EmptyRequests.EmptyRequest);
        app.MapPost("/auth", AuthRequests.OnAuth);
        app.MapPost("/auth/join", AuthRequests.AuthJoin);
        app.MapGet("/auth/hasjoined",
            (string username, string serverId) => AuthRequests.AuthHasJoined(username, serverId));

        app.MapGet("/clients", ClientRequests.GetClients);
        app.MapGet("/clients/{name}", ClientRequests.GetClientByName);
        app.MapPost("/clients", ClientRequests.CreateClient);
        app.MapPost("/clients/{name}", ClientRequests.LoadClient);
        app.MapDelete("/clients", ClientRequests.RemoveClient);

        app.MapGet("/versions", VersionRequests.GetVersions);

        app.MapGet("/launcher", LauncherRequests.GetLauncherInfo);

        return app;
    }

    public static WebApplication AddSwagger(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        return app;
    }
}