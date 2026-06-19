namespace ExtendFile.Panelis.Application.Modules.User.Requests.ConfirmPasswordReset;

public record ConfirmPasswordResetRequest(string Email, string Code, string NewPassword);
