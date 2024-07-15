namespace OAuth2OpenId.Application.Services.Interfaces;

public interface IEmailSender
{
    public Task SendEmailConfirmationEmailAsync(string code);
    public Task SendPasswordResetEmailAsync(string code);
    public Task SendMfaCodeEmailAsync(string code);
}