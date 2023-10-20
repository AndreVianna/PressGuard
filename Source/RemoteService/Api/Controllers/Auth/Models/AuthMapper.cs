namespace RemoteService.Controllers.Auth.Models;

internal static class AuthMapper {
    public static SignIn ToDomain(this LoginRequest request)
        => new() {
            Email = request.Email.Trim(),
            Password = request.Password.Trim(),
        };

    public static SignOn ToDomain(this RegisterRequest request)
        => new() {
            FirstName = request.FirstName?.Trim(),
            LastName = request.LastName?.Trim(),
            Email = request.Email.Trim(),
            Password = request.Password.Trim(),
        };

    public static LoginResponse ToLoginResponse(this string response)
        => new() { Token = response };

    public static UserRole ToDomain(this RoleRequest request, Guid userId)
        => new() {
            UserId = userId,
            Role = request.Role,
        };
}
