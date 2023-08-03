using AutoMapper;
using GamerVII.LauncherDomains.Models.Dto.GameClients;
using GamerVII.LauncherDomains.Models.Launcher;

namespace GamerVII.LaunchServer.Core.Profiles.ClientProfiles;

public class ClientProfiles : Profile
{
    public ClientProfiles()
    {
        CreateMap<Client, ReadGameClientDto>();
    }
}