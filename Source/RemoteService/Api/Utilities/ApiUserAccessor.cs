namespace RemoteService.Utilities;

[ExcludeFromCodeCoverage]
public class ApiUserAccessor : IUserAccessor {
    private readonly IEnumerable<Claim> _claims;

    public ApiUserAccessor(IHttpContextAccessor accessor) {
        var httpContext = Ensure.IsNotNull(accessor.HttpContext);
        _claims = httpContext.User.Claims;
    }

    public string Id => _claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                     ?? throw new InvalidOperationException("User id not found.");
    public string Email => _claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value
                        ?? throw new InvalidOperationException("User email not found.");
    public string BaseFolder => _claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData)?.Value
                           ?? throw new InvalidOperationException("FolderName not found.");
    public string Name => _claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value
                       ?? throw new InvalidOperationException("User name not found.");

    public ICollection<Role> Roles
        => _claims
          .Where(c => c.Type == ClaimTypes.Role)
          .Select(c => Enum.TryParse<Role>(c.Value, true, out var role) ? role : (Role?)null)
          .Where(c => c != null)
          .Select(c => c!.Value)
          .ToArray();
}
