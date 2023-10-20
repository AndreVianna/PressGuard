namespace Repository.Contracts;

public record LocalizedText(string Key, string? Value)
    : ILocalizedResource<LocalizedText> {
}
