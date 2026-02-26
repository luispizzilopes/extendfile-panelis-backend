namespace ExtendFile.Panelis.Application.Modules.House.Responses;

public class BoxDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid HouseId { get; set; }
}