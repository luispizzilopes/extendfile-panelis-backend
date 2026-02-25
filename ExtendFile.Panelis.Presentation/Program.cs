using ExtendFile.Panelis.Presentation.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.AddEnvironmentConfiguration(); 
builder.Services.AddConfigurations(builder.Configuration, builder.Environment); // Add services to the container in AddConfigurations.
builder.Host.UseSerilog();

var app = builder.Build();
app.AddRequestPipelineConfigurations(); // Configure the HTTP request pipeline in AddRequestPipelineConfigurations.
app.Run();