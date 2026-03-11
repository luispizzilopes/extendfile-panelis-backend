namespace ExtendFile.Panelis.Application.Modules.Test.Responses;

public class TestDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public Guid BoxId { get; set; }
    public DateTime TestDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public string HouseName { get; set; } = string.Empty;
    public string BoxName { get; set; } = string.Empty;
}
