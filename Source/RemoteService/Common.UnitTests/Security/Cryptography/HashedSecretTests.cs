namespace System.Security.Cryptography;

public class HashedSecretTests {
    [Fact]
    public void Constructor_WithStrings_CreatesObject() {
        var subject = new HashedSecret("SomeHash", "SomeSalt");

        subject.Should().NotBeNull();
        subject.Hash.Should().Be("SomeHash");
        subject.Salt.Should().Be("SomeSalt");
        subject.HashBytes.Should().BeEquivalentTo(new byte[] { 0x4A, 0x89, 0x9E, 0x1D, 0xAB, 0x21});
        subject.SaltBytes.Should().BeEquivalentTo(new byte[] { 0x4A, 0x89, 0x9E, 0x49, 0xA9, 0x6D});
    }

    [Fact]
    public void Constructor_WithByteArrays_CreatesObject() {
        var subject = new HashedSecret("SomeHash"u8.ToArray(), "SomeSalt"u8.ToArray());

        subject.Should().NotBeNull();
        subject.Hash.Should().Be("U29tZUhhc2g=");
        subject.Salt.Should().Be("U29tZVNhbHQ=");
        subject.HashBytes.Should().BeEquivalentTo("SomeHash"u8.ToArray());
        subject.SaltBytes.Should().BeEquivalentTo("SomeSalt"u8.ToArray());
    }

    [Theory]
    [InlineData("SomeHash", true)]
    [InlineData("Invalid", false)]
    public void Verify_CreatesObject(string input, bool expectedResult) {
        var hasher = Substitute.For<IHasher>();
        var other = new HashedSecret("Invalid"u8.ToArray(), "SomeSalt"u8.ToArray());
        hasher.HashSecret(Arg.Any<string>(), Arg.Any<byte[]>()).Returns(other);
        var correct = new HashedSecret("SomeHash"u8.ToArray(), "SomeSalt"u8.ToArray());
        hasher.HashSecret("SomeHash", Arg.Any<byte[]>()).Returns(correct);

        var subject = new HashedSecret("SomeHash"u8.ToArray(), "SomeSalt"u8.ToArray());

        var result = subject.Verify(input, hasher);

        result.Should().Be(expectedResult);
    }
}
