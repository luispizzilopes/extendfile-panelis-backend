namespace ExtendFile.Panelis.Application.Modules.Test.Responses.DeleteTest;

public record DeleteTestResponse
{
    public bool Success { get; init; }
    public string Message { get; init; } = string.Empty;
}
