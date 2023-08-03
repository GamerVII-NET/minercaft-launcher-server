using GamerVII.LauncherDomains.Models.Dto.Users;

namespace GamerVII.LaunchServer.Services.Auth;

public interface IAuthService
{
    Task<UserReadDto> AuthAsync(AuthUserDto user);
}