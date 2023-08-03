FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["GamerVII.LaunchServer/GamerVII.LaunchServer.csproj", "GamerVII.LaunchServer/"]
COPY ["GamerVII.LauncherDomains/GamerVII.LauncherDomains.csproj", "GamerVII.LauncherDomains/"]
RUN dotnet restore "GamerVII.LaunchServer/GamerVII.LaunchServer.csproj"
COPY . .
WORKDIR "/src/GamerVII.LaunchServer"
RUN dotnet build "GamerVII.LaunchServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GamerVII.LaunchServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GamerVII.LaunchServer.dll"]
