namespace RemoteService.Authentication;

public record SignIn : IValidatable {
    public required string Email { get; init; }
    public required string Password { get; init; }

    public Result Validate(IDictionary<string, object?>? context = null) {
        var result = Result.Success();
        result += Email.IsRequired()
                       .And().IsNotEmptyOrWhiteSpace()
                       .And().IsEmail().Result;
        result += Password.IsRequired()
                        .And().IsNotEmptyOrWhiteSpace()
                        .And().LengthIsAtMost(Validation.Password.MaximumLength).Result;
        return result;
    }
}
