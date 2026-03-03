namespace ExtendFile.Panelis.Application.Modules.User.Responses;

public class UserSessionResponse
{
    public string? Id { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public TokenJwtInformationResponse? TokenJwtInformation { get; set; }

    public UserSessionResponse() { }

    public UserSessionResponse(
        string? id, 
        string? email, 
        string? name,
        TokenJwtInformationResponse? tokenJwtInformation)
    {
        Id = id;
        Email = email;
        Name = name;
        TokenJwtInformation = tokenJwtInformation;
    }
}