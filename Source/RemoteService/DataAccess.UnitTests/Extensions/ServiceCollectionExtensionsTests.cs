namespace DataAccess.Extensions;

public class ServiceCollectionExtensionsTests {
    [Fact]
    public void AddDomainHandlers_RegisterHandlers() {
        var services = new ServiceCollection();

        var result = services.AddRepositories();

        result.Should().BeSameAs(services);
    }
}