namespace ExtendFile.Panelis.Application.Modules.User.Requests.TwoFactor;

public record VerifyTwoFactorRequest(string Email, string Code);
