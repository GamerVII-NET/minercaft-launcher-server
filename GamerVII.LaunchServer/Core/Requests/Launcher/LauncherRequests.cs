using AutoMapper;
using GamerVII.LauncherDomains.Models.Dto.Launcher;
using GamerVII.LaunchServer.Core.Repositories.Launcher;
using Microsoft.AspNetCore.Mvc;

namespace GamerVII.LaunchServer.Core.Requests.Launcher;

public class LauncherRequests
{
    public static async Task<IResult> GetLauncherInfo(ILauncherRepository launcherRepository, IMapper mapper)
    {
        var launcherData = await launcherRepository.GetReleaseInfo();

        return Results.Ok(mapper.Map<LauncherInfoDto>(launcherData));

    }
}