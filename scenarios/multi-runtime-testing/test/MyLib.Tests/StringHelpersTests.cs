using System;
using Xunit;

namespace MyLib.Tests;

public class StringHelpersTests
{
    [Theory]
    [InlineData("Hello, World!", 5, "He...")]
    [InlineData("Hi", 10, "Hi")]
    [InlineData("Hello", 5, "Hello")]
    [InlineData("Hello", 3, "Hel")]
    public void Truncate_ReturnsExpectedResult(string input, int maxLength, string expected)
    {
        var result = StringHelpers.Truncate(input, maxLength);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Truncate_NullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => StringHelpers.Truncate(null!, 5));
    }

    [Fact]
    public void Truncate_NegativeMaxLength_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => StringHelpers.Truncate("test", -1));
    }

    [Theory]
    [InlineData("Hello123", true)]
    [InlineData("abc", true)]
    [InlineData("123", true)]
    [InlineData("Hello World", false)]
    [InlineData("hello!", false)]
    [InlineData("", false)]
    public void IsAlphanumericAscii_ReturnsExpectedResult(string input, bool expected)
    {
        var result = StringHelpers.IsAlphanumericAscii(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void IsAlphanumericAscii_NullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => StringHelpers.IsAlphanumericAscii(null!));
    }
}
