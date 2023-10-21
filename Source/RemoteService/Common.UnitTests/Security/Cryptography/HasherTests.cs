namespace System.Security.Cryptography;

public class HasherTests {
    [Fact]
    public void Constructor_CreatesObject() {
        var subject = new Hasher();

        subject.Should().NotBeNull();
    }

    [Fact]
    public void HashSecret_CreatesObject() {
        var hasher = new Hasher();

        var subject = hasher.HashSecret("Text to be encrypted.");

        subject.Hash.Should().NotBeNull();
        subject.Salt.Should().NotBeNull();
    }
}