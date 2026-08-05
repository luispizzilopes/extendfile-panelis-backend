namespace ExtendFile.Panelis.Application.Modules.House.Requests.DeleteBoxDocument;

public class DeleteBoxDocumentRequest
{
    public Guid HouseId { get; set; }
    public Guid BoxId { get; set; }
    public string FileName { get; set; } = string.Empty;
}
