namespace RemoteService.Extensions;

public class ServiceCollectionExtensionsTests {
    [Fact]
    public void AddDomainHandlers_RegisterHandlers() {
        var services = new ServiceCollection();
        var configuration = Substitute.For<IConfiguration>();

        var result = services.AddDomainHandlers(configuration);

        result.Should().BeSameAs(services);
    }
}