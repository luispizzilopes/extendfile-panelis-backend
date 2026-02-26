namespace ExtendFile.Panelis.Application.Modules.House.Requests.UpdateBox;

public class UpdateBoxRequest
{
    public Guid HouseId { get; init; }
    public Guid Id { get; init; }
    public string Name { get; set; } = string.Empty;
}
