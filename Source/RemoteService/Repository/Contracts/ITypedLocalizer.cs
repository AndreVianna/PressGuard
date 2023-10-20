using Repository.Models;

namespace Repository.Contracts;

public interface ITypedLocalizer : ILocalizer {
    static abstract ResourceType Type { get; }
}
