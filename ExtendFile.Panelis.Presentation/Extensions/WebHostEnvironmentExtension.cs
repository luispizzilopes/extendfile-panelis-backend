namespace ExtendFile.Panelis.Presentation.Extensions;

public static class WebHostEnvironmentExtension
{
    public static bool IsLocalhost(this IWebHostEnvironment env)
    {
        return env.EnvironmentName
            .Equals("localhost", StringComparison.CurrentCultureIgnoreCase);
    }
}