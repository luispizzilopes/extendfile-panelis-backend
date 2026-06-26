namespace ExtendFile.Panelis.Application.Modules.User.Responses;

public class UserSessionResponse
{
    public string? Id { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public TokenJwtInformationResponse? TokenJwtInformation { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public bool RequiresTwoFactor { get; set; }

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

    public UserSessionResponse(
        string? id,
        string? email,
        string? name,
        TokenJwtInformationResponse? tokenJwtInformation,
        bool twoFactorEnabled)
    {
        Id = id;
        Email = email;
        Name = name;
        TokenJwtInformation = tokenJwtInformation;
        TwoFactorEnabled = twoFactorEnabled;
    }
}