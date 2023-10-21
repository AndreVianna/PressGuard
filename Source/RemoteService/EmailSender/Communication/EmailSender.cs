using RemoteService.Authentication;

namespace RemoteService.Communication;
public class EmailSender : IEmailSender {
    public Task SendEmailConfirmationMessage(User user, CancellationToken ct = default) => Task.CompletedTask;
}
