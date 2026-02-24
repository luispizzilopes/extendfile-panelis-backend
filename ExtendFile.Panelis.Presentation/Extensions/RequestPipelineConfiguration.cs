namespace ExtendFile.Panelis.Presentation.Extensions;

public static class RequestPipelineConfiguration
{
    public static void AddRequestPipelineConfigurations(this WebApplication app)
    {
        if (!app.Environment.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
    }
}