namespace DataAccess.Repositories.Auth;

public static class UserMapper {
    public static UserData ToData(this User input)
        => new() {
            Id = input.Id,

            Email = input.Email,
            HashedPassword = input.HashedPassword,
            LockExpiration = input.LockExpiration,
            SignInRetryCount = input.SignInRetryCount,
            IsBlocked = input.IsBlocked,
            Roles = input.Roles,
            FirstName = input.FirstName,
            LastName = input.LastName,
            Birthday = input.Birthday,

            ChangeStamp = input.ChangeStamp,
        };

    public static UserRow ToRow(this UserData input)
        => new() {
            Id = input.Id,
            Email = input.Email,
            Name = GetFullName(input),
        };

    private static string GetFullName(UserData input) 
        => $"{input.FirstName}{(input.FirstName is not null && input.LastName is not null ? " " : string.Empty)}{input.LastName}";

    public static User? ToModel(this UserData? input)
        => input is null
            ? null
            : new() {
                Id = input.Id,

                Email = input.Email,
                HashedPassword = input.HashedPassword,
                LockExpiration = input.LockExpiration,
                SignInRetryCount = input.SignInRetryCount,
                IsBlocked = input.IsBlocked,
                Roles = input.Roles.ToHashSet(),
                FirstName = input.FirstName,
                LastName = input.LastName,
                Birthday = input.Birthday,

                ChangeStamp = input.ChangeStamp,
            };
}
