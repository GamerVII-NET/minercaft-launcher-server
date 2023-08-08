using GamerVII.LaunchServer.Core.Extensions.App;

var builder = WebApplication
    .CreateBuilder(args)
    .AddDatabaseContext()
    .AddServices()
    .AddAutoMapper();

var app = builder
    .Build()
    .AddRoutes()
    .AddSwagger();

app.Run();