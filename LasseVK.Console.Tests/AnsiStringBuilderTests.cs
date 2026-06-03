using System.Text;

using LasseVK.Console.Ansi;

namespace LasseVK.Console.Tests;

public class AnsiStringBuilderTests
{
    [Theory]
    [InlineData(0, "")]
    [InlineData(1, "\e[A")]
    [InlineData(2, "\e[2A")]
    [InlineData(17, "\e[17A")]
    public void MoveUp_WithTestCases(int amount, string expected)
    {
        var writer = new AnsiStringBuilder(new StringBuilder());
        string output = writer.MoveUp(amount).Target.ToString();

        Assert.Equal(expected, output);
    }

    [Theory]
    [InlineData(0, "")]
    [InlineData(1, "\e[B")]
    [InlineData(2, "\e[2B")]
    [InlineData(17, "\e[17B")]
    public void MoveDown_WithTestCases(int amount, string expected)
    {
        var writer = new AnsiStringBuilder(new StringBuilder());
        string output = writer.MoveDown(amount).Target.ToString();

        Assert.Equal(expected, output);
    }

    [Theory]
    [InlineData(0, "")]
    [InlineData(1, "\e[D")]
    [InlineData(2, "\e[2D")]
    [InlineData(17, "\e[17D")]
    public void MoveLeft_WithTestCases(int amount, string expected)
    {
        var writer = new AnsiStringBuilder(new StringBuilder());
        string output = writer.MoveLeft(amount).Target.ToString();

        Assert.Equal(expected, output);
    }

    [Theory]
    [InlineData(0, "")]
    [InlineData(1, "\e[C")]
    [InlineData(2, "\e[2C")]
    [InlineData(17, "\e[17C")]
    public void MoveRight_WithTestCases(int amount, string expected)
    {
        var writer = new AnsiStringBuilder(new StringBuilder());
        string output = writer.MoveRight(amount).Target.ToString();

        Assert.Equal(expected, output);
    }

    [Theory]
    [InlineData(0, "\e[G")]
    [InlineData(1, "\e[E")]
    [InlineData(2, "\e[2E")]
    [InlineData(17, "\e[17E")]
    public void MoveBeginningOfLinesDown_WithTestCases(int amount, string expected)
    {
        var writer = new AnsiStringBuilder(new StringBuilder());
        string output = writer.MoveBeginningOfLinesDown(amount).Target.ToString();

        Assert.Equal(expected, output);
    }

    [Theory]
    [InlineData(0, "\e[G")]
    [InlineData(1, "\e[F")]
    [InlineData(2, "\e[2F")]
    [InlineData(17, "\e[17F")]
    public void MoveBeginningOfLinesUp_WithTestCases(int amount, string expected)
    {
        var writer = new AnsiStringBuilder(new StringBuilder());
        string output = writer.MoveBeginningOfLinesUp(amount).Target.ToString();

        Assert.Equal(expected, output);
    }

}