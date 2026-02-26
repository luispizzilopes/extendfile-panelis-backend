namespace ExtendFile.Panelis.Application.Modules.House.Requests.CreateBox;

public class CreateBoxRequest
{
    public Guid HouseId { get; init; }
    public string Name { get; set; } = string.Empty;
}