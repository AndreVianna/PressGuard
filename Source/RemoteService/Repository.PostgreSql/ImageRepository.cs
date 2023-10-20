using Repository.Contracts;
using Repository.PostgreSql.Schema;

namespace Repository.PostgreSql;

internal partial class PostgreSqlLocalizationRepository {
    public LocalizedImage? FindImageByKey(string imageKey)
        => GetOrDefault<Image, LocalizedImage>(imageKey);

    public void AddOrUpdateImage(LocalizedImage input)
        => AddOrUpdate<Image, LocalizedImage>(input);
}
