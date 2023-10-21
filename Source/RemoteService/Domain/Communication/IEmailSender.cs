using RemoteService.Authentication;

namespace RemoteService.Communication;

public interface IEmailSender {
    Task SendEmailConfirmationMessage(User user, CancellationToken ct);
}