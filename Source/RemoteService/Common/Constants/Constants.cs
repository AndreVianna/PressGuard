namespace System.Constants;

public static class Constants {
    public static class Commands {
        public const string IsEqualTo = "IsEqualTo";
        public const string Contains = "Contains";
        public const string ContainsKey = "ContainsKey";
        public const string ContainsValue = "ContainsValue";
        public const string Has = "Has";
        public const string IsAfter = "IsAfter";
        public const string IsBefore = "IsBefore";
        public const string IsEmpty = "IsEmpty";
        public const string IsEmptyOrWhiteSpace = "IsEmptyOrWhiteSpace";
        public const string IsGreaterThan = "IsGreaterThan";
        public const string IsLessThan = "IsLessThan";
        public const string IsNull = "IsNull";
        public const string IsIn = "IsIn";
        public const string IsEmail = "IsEmail";
        public const string IsPassword = "IsPassword";
        public const string IsValid = "IsValid";
        public const string LengthIs = "LengthIs";
        public const string HasAtMost = "HasAtMost";
        public const string LengthIsAtMost = "LengthIsAtMost";
        public const string HasAtLeast = "HasAtLeast";
        public const string LengthIsAtLeast = "LengthIsAtLeast";
    }

    public static class ErrorMessages {
        public static string GetInvertedErrorMessage(string message, params object?[] args)
            => GetErrorMessage(InvertMessage(message), args);

        public static string GetErrorMessage(string message, params object?[] args)
            => string.Format(message, args);

        public static string InvertMessage(string message) => message switch {
            _ when message.Contains(" cannot ") => message.Replace(" cannot ", " must "),
            _ when message.Contains(" must ") => message.Replace(" must ", " cannot "),
            _ when message.Contains(" is not ") => message.Replace(" is not ", " is "),
            _ when message.Contains(" is ") => message.Replace(" is ", " is not "),
            _ => message
        };

        public const string MustBeAfter = "'{0}' must be after {1}. Found: {2}.";
        public const string MustBeBefore = "'{0}' must be before {1}. Found: {2}.";
        public const string MustBeEmpty = "'{0}' must be empty.";
        public const string MustBeEmptyOrWhitespace = "'{0}' must be empty or whitespace.";
        public const string MustBeEqualTo = "'{0}' must be equal to {1}. Found: {2}.";
        public const string MustBeGraterThan = "'{0}' must be greater than {1}. Found: {2}.";
        public const string MustBeIn = "'{0}' must be one of these: '{1}'. Found: {2}.";
        public const string MustBeLessThan = "'{0}' must be less than {1}. Found: {2}.";
        public const string MustBeNull = "'{0}' must be null.";
        public const string MustContain = "'{0}' must contain '{1}'.";
        public const string MustContainValue = "'{0}' must contain the value '{1}'.";
        public const string MustContainKey = "'{0}' must contain the key '{1}'.";
        public const string MustContainNull = "'{0}' must contain null item(s).";
        public const string MustContainNullOrEmpty = "'{0}' must contain null or empty string(s).";
        public const string MustContainNullOrWhitespace = "'{0}' must contain null or whitespace string(s).";
        public const string MustHaveACountOf = "'{0}' count must be {1}. Found: {2}.";
        public const string MustHaveALengthOf = "'{0}' length must be {1}. Found: {2}.";
        public const string MustHaveAMaximumCountOf = "'{0}' maximum count must be {1}. Found: {2}.";
        public const string MustHaveAMaximumLengthOf = "'{0}' maximum length must be {1}. Found: {2}.";
        public const string MustHaveAMinimumCountOf = "'{0}' minimum count must be {1}. Found: {2}.";
        public const string MustHaveAMinimumLengthOf = "'{0}' minimum length must be {1}. Found: {2}.";
        public const string MustBeValid = "'{0}' must be valid.";
        public const string MustBeAValidEmail = "'{0}' must be a valid email.";
        public const string MustBeAValidPassword = "'{0}' must be a valid password.";
        public const string MustBeOfType = "'{0}' must be of type '{1}'. Found: '{2}'.";
    }
}
