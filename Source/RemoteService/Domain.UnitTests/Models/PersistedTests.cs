namespace RemoteService.Models;

public class PersistedTests {
    private record TestPersisted : Persisted;

    [Fact]
    public void Constructor_CreatesInstance() {
        var id = Guid.NewGuid();
        var subject = new TestPersisted {
            Id = id,
        };

        subject.Id.Should().Be(id);
        subject.State.Should().Be(State.New);
    }
}
