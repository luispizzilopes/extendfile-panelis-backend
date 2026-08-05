using Microsoft.AspNetCore.Http;

namespace ExtendFile.Panelis.Application.Modules.House.Requests.UploadBoxDocument;

public class UploadBoxDocumentRequest
{
    public Guid HouseId { get; set; }
    public Guid BoxId { get; set; }
    public IFormFile File { get; set; } = null!;
}
