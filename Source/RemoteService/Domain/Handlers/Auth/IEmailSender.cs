namespace RemoteService.Handlers.Auth;

public interface IEmailSender {
    Task SendEmailConfirmationMessage(User user, CancellationToken ct);
}