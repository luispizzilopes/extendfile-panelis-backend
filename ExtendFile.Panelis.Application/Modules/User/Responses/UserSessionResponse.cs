namespace ExtendFile.Panelis.Application.Modules.User.Responses;

public class UserSessionResponse
{
    public string? Id { get; set; }
    public string? Email { get; set; }
    public TokenJwtInformationResponse? TokenJwtInformation { get; set; }

    public UserSessionResponse() { }

    public UserSessionResponse(string? id, string? email, TokenJwtInformationResponse? tokenJwtInformation)
    {
        Id = id;
        Email = email;
        TokenJwtInformation = tokenJwtInformation;
    }
}