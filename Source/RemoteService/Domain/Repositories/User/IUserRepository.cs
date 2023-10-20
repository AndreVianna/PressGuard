using RemoteService.Handlers.Auth;

using UserModel = RemoteService.Handlers.Auth.User;

namespace RemoteService.Repositories.User;

public interface IUserRepository : IRepository<Handlers.Auth.User, UserRow> {
    Task<UserModel?> VerifyAsync(SignIn signIn, CancellationToken cancellation = default);
}
