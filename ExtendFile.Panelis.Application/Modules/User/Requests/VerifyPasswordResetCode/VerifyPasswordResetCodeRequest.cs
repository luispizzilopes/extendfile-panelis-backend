namespace ExtendFile.Panelis.Application.Modules.User.Requests.VerifyPasswordResetCode;

public record VerifyPasswordResetCodeRequest(string Email, string Code);
