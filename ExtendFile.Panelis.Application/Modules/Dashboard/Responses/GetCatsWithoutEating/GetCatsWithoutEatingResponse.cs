namespace ExtendFile.Panelis.Application.Modules.Dashboard.Responses.GetCatsWithoutEating;

public record GetCatsWithoutEatingResponse
{
    public List<CatWithoutEatingDto> Cats { get; init; } = new();
}

public record CatWithoutEatingDto
{
    public Guid CatId { get; init; }
    public string CatName { get; init; } = string.Empty;
    public string HouseName { get; init; } = string.Empty;
    public string BoxName { get; init; } = string.Empty;
    public int DaysWithoutEating { get; init; }
    public string Status { get; init; } = string.Empty; 
}
