using AutoMapper;
using GamerVII.LauncherDomains.Models.Data.Models;
using GamerVII.LauncherDomains.Models.Dto.Launcher;

namespace GamerVII.LaunchServer.Core.Profiles.LauncherProfiles;

public class LauncherProfiles : Profile
{
    public LauncherProfiles()
    {
        CreateMap<Launcher, LauncherInfoDto>();
    }
}