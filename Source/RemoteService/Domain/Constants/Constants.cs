namespace RemoteService.Constants;

public static class Validation {
    public static class Name {
        public const int MaximumLength = 100;
        public const int MinimumLength = 3;
    }

    public static class Description {
        public const int MaximumLength = 1000;
        public const int MinimumLength = 10;
    }

    public static class ShortName {
        public const int MaximumLength = 10;
        public const int MinimumLength = 2;
    }

    public static class Tag {
        public const int MaximumLength = 20;
    }

    public static class Password {
        public const int MaximumLength = 100;
    }
}
