using System.Security;

namespace RemoteService.Security;

public class PasswordPolicy : IPasswordPolicy {
    public Result Enforce(string password) => Result.Success();
}
