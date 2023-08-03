using GamerVII.LaunchServer.Extensions.App;

var builder = WebApplication
    .CreateBuilder(args)
    .AddServices()
    .AddAutoMapper();

var app = builder
    .Build()
    .AddRoutes()
    .AddSwagger();

app.Run();