namespace ExtendFile.Panelis.Application.Modules.User.Responses;

public class TokenJwtInformationResponse
{
    public string? Token { get; set; }
    public DateTimeOffset? DateExpiration { get; set; }

    public TokenJwtInformationResponse() { }

    public TokenJwtInformationResponse(string? token, DateTimeOffset? dateExpiration)
    {
        Token = token;
        DateExpiration = dateExpiration;
    }
}