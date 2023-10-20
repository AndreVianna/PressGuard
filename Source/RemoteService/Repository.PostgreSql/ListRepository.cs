using Repository.Contracts;
using Repository.PostgreSql.Schema;

namespace Repository.PostgreSql;

internal partial class PostgreSqlLocalizationRepository {
    public LocalizedList? FindListByKey(string listKey)
        => GetOrDefault<List, LocalizedList>(listKey);

    public void AddOrUpdateList(LocalizedList input)
        => AddOrUpdate<List, LocalizedList>(input);
}
