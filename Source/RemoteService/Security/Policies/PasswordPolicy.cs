namespace RemoteService.Policies;

public class PasswordPolicy : IPasswordPolicy {
    public Result Enforce(string password) => Result.Success();
}
