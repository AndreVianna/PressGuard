using RemoteService.Repositories.User;

using static System.Results.SignInResult;

namespace RemoteService.Handlers.Auth;

public class AuthHandler : CrudHandler<User, UserRow, IUserRepository>, IAuthHandler {
    private readonly AuthSettings _authSettings;
    private readonly IHasher _hasher;
    private readonly IEmailSender _emailSender;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly ILogger<AuthHandler> _logger;

    public AuthHandler(IUserRepository repository,
                       IHasher hasher,
                       IOptions<AuthSettings> authSettings,
                       IEmailSender emailSender,
                       ITokenGenerator tokenGenerator,
                       ILogger<AuthHandler> logger)
        : base(repository) {
        _hasher = hasher;
        _authSettings = authSettings.Value;
        _emailSender = emailSender;
        _tokenGenerator = tokenGenerator;
        _logger = logger;
    }

    public async Task<SignInResult> SignInAsync(SignIn signIn, CancellationToken ct = default) {
        var validation = signIn.Validate();
        if (validation.IsInvalid) {
            _logger.LogDebug("Login attempt with invalid request.");
            return Invalid(validation);
        }

        var user = await Repository.VerifyAsync(signIn, ct);
        if (user is null) {
            _logger.LogDebug("Login attempt for '{email}' failed.", signIn.Email);
            return Failure();
        }

        var token = _tokenGenerator.GenerateSignInToken(user);
        if (!user.IsConfirmed) {
            _logger.LogDebug("Login for '{email}' ok, but email is not confirmed.", signIn.Email);
            return ConfirmationRequired(token);
        }

        if (_authSettings.Requires2Factor) {
            _logger.LogDebug("Login for '{email}' ok, but requires two-factor.", signIn.Email);
            return TwoFactorRequired(token);
        }

        _logger.LogDebug("Login for '{email}' succeeded.", signIn.Email);
        return Success(token);
    }

    public async Task<CrudResult> RegisterAsync(SignOn signOn, CancellationToken ct = default) {
        var validation = signOn.Validate();
        if (validation.IsInvalid) {
            _logger.LogDebug("Register attempt with invalid request.");
            return CrudResult.Invalid(validation);
        }

        var user = new User {
            Id = Guid.NewGuid(),
            FirstName = signOn.FirstName,
            LastName = signOn.LastName,
            Email = signOn.Email,
            HashedPassword = _hasher.HashSecret(signOn.Password),
        };

        var addedUser = await Repository.AddAsync(user, ct);

        if (addedUser is null) return CrudResult.Conflict();

        await _emailSender.SendEmailConfirmationMessage(addedUser, ct);

        return CrudResult.Success();
    }

    public async Task<CrudResult> GrantRoleAsync(UserRole userRole, CancellationToken ct = default) {
        var user = await Repository.GetByIdAsync(userRole.UserId, ct);
        if (user is null) return CrudResult.NotFound();

        user.Roles.Add(userRole.Role);
        await UpdateAsync(user, ct);

        return CrudResult.Success();
    }

    public async Task<CrudResult> RevokeRoleAsync(UserRole userRole, CancellationToken ct = default) {
        var user = await Repository.GetByIdAsync(userRole.UserId, ct);
        if (user is null) return CrudResult.NotFound();

        user.Roles.Remove(userRole.Role);
        await UpdateAsync(user, ct);

        return CrudResult.Success();
    }
}
