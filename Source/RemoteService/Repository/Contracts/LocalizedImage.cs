namespace Repository.Contracts;

public record LocalizedImage(string Key, byte[] Bytes)
    : ILocalizedResource<LocalizedImage> {
}
