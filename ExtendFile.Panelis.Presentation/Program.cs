using ExtendFile.Panelis.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddEnvironmentConfiguration(); 
builder.Services.AddConfigurations(builder.Configuration, builder.Environment); // Add services to the container in AddConfigurations.

var app = builder.Build();
app.AddRequestPipelineConfigurations(); // Configure the HTTP request pipeline in AddRequestPipelineConfigurations.
app.Run();