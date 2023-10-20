using Repository.PostgreSql.Schema;

using Application = Repository.PostgreSql.Schema.Application;

namespace Repository.PostgreSql;

public class LocalizationDbContext : DbContext {
    public LocalizationDbContext(DbContextOptions<LocalizationDbContext> options)
        : base(options) {
    }

    public DbSet<Application> Applications { get; set; } = null!;
    public DbSet<Text> Texts { get; set; } = null!;
    public DbSet<Image> Images { get; set; } = null!;
    public DbSet<List> Lists { get; set; } = null!;
}
