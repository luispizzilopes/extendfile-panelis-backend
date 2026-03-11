using Microsoft.AspNetCore.Http;

namespace ExtendFile.Panelis.Application.Modules.Test.Requests.CreateTest;

public class CreateTestRequest
{
    public Guid BoxId { get; set; }
    public IFormFile File { get; set; } = null!;
    public bool? ValidateDateFile { get; set; } 
}