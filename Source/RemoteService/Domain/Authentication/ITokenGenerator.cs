namespace RemoteService.Authentication;

public interface ITokenGenerator {
    string GenerateSignInToken(User user);
    string GenerateEmailConfirmationToken(User user);
}