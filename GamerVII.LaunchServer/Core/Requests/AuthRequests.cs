using GamerVII.LauncherDomains.Models.Dto.Server;
using GamerVII.LauncherDomains.Models.Dto.Users;
using GamerVII.LaunchServer.Services.Auth;
using Newtonsoft.Json;

namespace GamerVII.LaunchServer.Core.Requests;

public static class AuthRequests
{
    public static async Task<IResult> OnAuth(IAuthService authService, AuthUserDto user)
    {
        var authUser = await authService.AuthAsync(user);

        return Results.Ok(authUser);
    }

    public static Task<IResult> AuthJoin(IAuthService authService, ServerAuthDto serverUser)
    {
        return Task.FromResult(Results.Ok());
        
        return Task.FromResult(Results.Ok(new ServerAuthError
        {
            cause = "Причина ошибки (опционально)",
            errorMessage = "Подробное описание, ОТОБРАЖАЕМОЕ В КЛИЕНТЕ!",
            error = "Короткое описание ошибки"
        }));
    }


    public static Task<IResult> AuthHasJoined(string userName, string serverId)
    {
        string userUuid = "31f5f477-53db-4afd-b88d-2e01815f4887";
        string skinPath =
            "http://textures.minecraft.net/texture/74d1e08b0bb7e9f590af27758125bbed1778ac6cef729aedfcb9613e9911ae75";
        string clockPath =
            "http://textures.minecraft.net/texture/74d1e08b0bb7e9f590af27758125bbed1778ac6cef729aedfcb9613e9911ae75";
            var serverHasJoinedDto = new ServerHasJoinedDto
        {
            id = userUuid,
            name = userName,
            properties = new List<Property>
            {
                new Property
                {
                    name = "textures",
                    value = Base64Encode(GetHash(userName, userUuid, skinPath, clockPath))
                }
            },
            profileActions = new ProfileActions()
        };


        return Task.FromResult(Results.Ok(serverHasJoinedDto));
    }

    private static string GetHash(string userName, string userUuid, string skinUrl, string cloakUrl)
    {
        return JsonConvert.SerializeObject(new TextureProperties
        {
            timestamp = 1691047818374,
            profileId = userUuid,
            profileName = userName,
            textures = new Textures
            {
                SKIN = new SKIN
                {
                    url = skinUrl
                },
                CAPE = new CAPE
                {
                    url = cloakUrl
                }
            }
        });
    }

    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }
}