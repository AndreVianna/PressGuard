namespace RemoteService.Handlers.Auth;

public interface ITokenGenerator {
    string GenerateSignInToken(User user);
    string GenerateEmailConfirmationToken(User user);
}