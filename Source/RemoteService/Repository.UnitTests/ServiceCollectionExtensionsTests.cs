using Repository.Contracts;
using Repository.Extensions;

namespace Repository;

public class ServiceCollectionExtensionsTests {
    // ReSharper disable once ClassNeverInstantiated.Local
    private class DummyFactory : ILocalizationRepositoryFactory {
        public ILocalizationRepository CreateFor(string culture) => throw new NotImplementedException();
    }

    [Fact]
    public void AddRepository_RegisterServices() {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var result = services.AddRepository<DummyFactory, LocalizationRepositoryOptions>();

        // Assert
        result.Should().BeSameAs(services);
    }
}
