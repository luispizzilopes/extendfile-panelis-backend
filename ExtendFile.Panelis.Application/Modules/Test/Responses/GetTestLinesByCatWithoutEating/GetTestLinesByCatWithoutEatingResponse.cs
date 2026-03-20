namespace ExtendFile.Panelis.Application.Modules.Test.Responses.GetTestLinesByCatWithoutEating;

public class GetTestLinesByCatWithoutEatingResponse
{
    public List<TestLineDto> TestLines { get; set; } = new();
    public int DaysWithoutEating { get; set; }
}
