var builder = WebApplication.CreateBuilder(args);

builder.AddServices();

var app = builder.Build();

await app.UseMiddlewares();