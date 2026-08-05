using System.Net.Http.Json;
using ExtendFile.Panelis.Application.Interfaces.Clients;

namespace ExtendFile.Panelis.Infrastructure.Clients;

public class FileListClient : IFileListClient
{
    private readonly HttpClient _httpClient;

    public FileListClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UploadFileToFolderResult> UploadFileToFolderAsync(
        string folder,
        string fileName,
        Stream fileStream,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        using var content = new MultipartFormDataContent();

        content.Add(new StringContent(folder), "Folder");

        var fileContent = new StreamContent(fileStream);
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
        content.Add(fileContent, "File", fileName);

        var response = await _httpClient.PostAsync("api/v1/Integration/by-folder", content, cancellationToken);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<UploadFileResponse>(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("Resposta inválida do serviço de arquivos.");

        return new UploadFileToFolderResult(result.Key, result.Url);
    }

    public async Task DeleteFileFromFolderAsync(
        string folder,
        string fileName,
        CancellationToken cancellationToken = default)
    {
        var url = $"api/v1/Integration/by-folder?folder={Uri.EscapeDataString(folder)}&fileName={Uri.EscapeDataString(fileName)}";
        var response = await _httpClient.DeleteAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    private sealed record UploadFileResponse(string Key, string Url);
}
