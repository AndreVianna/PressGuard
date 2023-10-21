using FluentAssertions;

using RemoteService.Authentication;

namespace RemoteService.Communication;

public class EmailSenderTests {
    [Fact]
    public async Task SendEmailConfirmationMessage_ReturnsTask() {
        // Arrange
        var sender = new EmailSender();
        var user = new User { 
            Email = "user@email.com",
        };

        // Act
        var action = () => sender.SendEmailConfirmationMessage(user);

        // Assert
        await action.Should().NotThrowAsync();
    }
}
