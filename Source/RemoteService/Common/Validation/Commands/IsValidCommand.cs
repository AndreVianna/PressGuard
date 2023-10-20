namespace System.Validation.Commands;

public sealed class IsValidCommand
    : ValidationCommand {
    public IsValidCommand(string source)
        : base(source) {
    }

    public override Result Validate(object? subject) {
        var result = Result.Success();
        if (subject is not IValidatable v) return result;
        var validation = v.Validate();
        foreach (var error in validation.Errors) {
            error.Arguments[0] = $"{Source}.{error.Arguments[0]}";
            result += error;
        }

        return result;
    }
}