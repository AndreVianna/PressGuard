using RemoteService.Authentication;

namespace RemoteService.Extensions;

public class ServiceCollectionExtensionsTests {
    // ReSharper disable once ClassNeverInstantiated.Local - Used for testing
    private class DummyTokenGenerator : ITokenGenerator {
        public string GenerateEmailConfirmationToken(User user) => throw new NotImplementedException();
        public string GenerateSignInToken(User user) => throw new NotImplementedException();
    }

    [Fact]
    public void AddDomainHandlers_RegisterHandlers() {
        var services = new ServiceCollection();
        var configuration = Substitute.For<IConfiguration>();

        var result = services.AddDomainHandlers<DummyTokenGenerator>(configuration);

        result.Should().BeSameAs(services);
    }
}
