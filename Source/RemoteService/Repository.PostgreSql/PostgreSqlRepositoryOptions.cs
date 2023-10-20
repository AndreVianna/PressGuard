using Repository.Contracts;

namespace Repository.PostgreSql;

public record PostgreSqlRepositoryOptions : LocalizationRepositoryOptions {
    [Required]
    public required string ConnectionString { get; init; }
}
