using Repository.PostgreSql.Extensions;

namespace Repository.PostgreSql;

public class ServiceCollectionExtensionsTests {
    [Fact]
    public void AddRepository_RegisterServices() {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var result = services.AddPostgreSqlRepository();

        // Assert
        result.Should().BeSameAs(services);
    }
}
