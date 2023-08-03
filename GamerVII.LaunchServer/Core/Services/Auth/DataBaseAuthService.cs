using GamerVII.LauncherDomains.Models.Dto.Users;

namespace GamerVII.LaunchServer.Core.Services.Auth;

public class DataBaseAuthService : IAuthService
{
    public Task<UserReadDto> AuthAsync(AuthUserDto user)
    {
        return Task.FromResult(new UserReadDto
        {
            Login = "GamerVII",
            AccessToken = "feosfeuchnfw3g4nh9crhw34icrnhicuerhfnicw4i",
            UUID = "31f5f47753db4afdb88d2e01815f4887"
        });

    }
}