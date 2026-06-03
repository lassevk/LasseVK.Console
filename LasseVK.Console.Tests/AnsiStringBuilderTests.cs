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
}