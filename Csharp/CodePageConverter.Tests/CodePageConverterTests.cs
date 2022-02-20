namespace CodePageConverter.Tests;

public class CodePageConverterTests
{
    [Fact]
    public void NoInputFile_ShouldBeFalse()
    {
        Arguments arguments = new(Array.Empty<string>());
        Assert.False(arguments.Check());
    }
}