using GamerVII.LauncherDomains.Models.Dto.Users;

namespace GamerVII.LaunchServer.Core.Services.Auth;

public interface IAuthService
{
    Task<UserReadDto> AuthAsync(AuthUserDto user);
}