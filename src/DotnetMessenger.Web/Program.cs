using DotnetMessenger.Web.Startup;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();

var app = builder.Build();

await app.ConfigureAsync();

app.Run();

public partial class Program;