using RemoteService.Authentication;

namespace RemoteService.Handlers.Auth;

public interface IAuthHandler : ICrudHandler<User, UserRow> {
    Task<SignInResult> SignInAsync(SignIn signIn, CancellationToken cancellation = default);
    Task<CrudResult> RegisterAsync(SignOn signOn, CancellationToken cancellation = default);

    Task<CrudResult> GrantRoleAsync(UserRole userRole, CancellationToken cancellation = default);
    Task<CrudResult> RevokeRoleAsync(UserRole userRole, CancellationToken cancellation = default);
}