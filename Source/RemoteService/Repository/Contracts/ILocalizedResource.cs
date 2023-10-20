using Repository.Models;

namespace Repository.Contracts;

public interface ILocalizedResource<TResource> : ILocalizedResource where TResource : ILocalizedResource<TResource> {
    public static ResourceType Type => Enum.Parse<ResourceType>(typeof(TResource).Name.AsSpan()[9..]);
}

public interface ILocalizedResource {
    string Key { get; }
}
