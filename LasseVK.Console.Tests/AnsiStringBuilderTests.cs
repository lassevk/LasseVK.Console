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

        Assert.Equal(expected.Replace("\e", "\\e"), output.Replace("\e", "\\e"));
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

        Assert.Equal(expected.Replace("\e", "\\e"), output.Replace("\e", "\\e"));
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

        Assert.Equal(expected.Replace("\e", "\\e"), output.Replace("\e", "\\e"));
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

        Assert.Equal(expected.Replace("\e", "\\e"), output.Replace("\e", "\\e"));
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

        Assert.Equal(expected.Replace("\e", "\\e"), output.Replace("\e", "\\e"));
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

        Assert.Equal(expected.Replace("\e", "\\e"), output.Replace("\e", "\\e"));
    }

    [Theory]
    [InlineData(AnsiColor.Black, "\e[38;5;0m")]
    [InlineData(AnsiColor.Red, "\e[38;5;1m")]
    [InlineData(AnsiColor.Green, "\e[38;5;2m")]
    [InlineData(AnsiColor.Yellow, "\e[38;5;3m")]
    [InlineData(AnsiColor.Blue, "\e[38;5;4m")]
    [InlineData(AnsiColor.Magenta, "\e[38;5;5m")]
    [InlineData(AnsiColor.Cyan, "\e[38;5;6m")]
    [InlineData(AnsiColor.White, "\e[38;5;7m")]
    [InlineData(AnsiColor.Black | AnsiColor.HighIntensity, "\e[38;5;8m")]
    [InlineData(AnsiColor.Red | AnsiColor.HighIntensity, "\e[38;5;9m")]
    [InlineData(AnsiColor.Green | AnsiColor.HighIntensity, "\e[38;5;10m")]
    [InlineData(AnsiColor.Yellow | AnsiColor.HighIntensity, "\e[38;5;11m")]
    [InlineData(AnsiColor.Blue | AnsiColor.HighIntensity, "\e[38;5;12m")]
    [InlineData(AnsiColor.Magenta | AnsiColor.HighIntensity, "\e[38;5;13m")]
    [InlineData(AnsiColor.Cyan | AnsiColor.HighIntensity, "\e[38;5;14m")]
    [InlineData(AnsiColor.White | AnsiColor.HighIntensity, "\e[38;5;15m")]
    public void SetForegroundColor_WithTestCases(AnsiColor color, string expected)
    {
        var writer = new AnsiStringBuilder(new StringBuilder());
        string output = writer.SetForegroundColor(color).Target.ToString();

        Assert.Equal(expected.Replace("\e", "\\e"), output.Replace("\e", "\\e"));
    }

    [Theory]
    [InlineData(AnsiColor.Black, "\e[48;5;0m")]
    [InlineData(AnsiColor.Red, "\e[48;5;1m")]
    [InlineData(AnsiColor.Green, "\e[48;5;2m")]
    [InlineData(AnsiColor.Yellow, "\e[48;5;3m")]
    [InlineData(AnsiColor.Blue, "\e[48;5;4m")]
    [InlineData(AnsiColor.Magenta, "\e[48;5;5m")]
    [InlineData(AnsiColor.Cyan, "\e[48;5;6m")]
    [InlineData(AnsiColor.White, "\e[48;5;7m")]
    [InlineData(AnsiColor.Black | AnsiColor.HighIntensity, "\e[48;5;8m")]
    [InlineData(AnsiColor.Red | AnsiColor.HighIntensity, "\e[48;5;9m")]
    [InlineData(AnsiColor.Green | AnsiColor.HighIntensity, "\e[48;5;10m")]
    [InlineData(AnsiColor.Yellow | AnsiColor.HighIntensity, "\e[48;5;11m")]
    [InlineData(AnsiColor.Blue | AnsiColor.HighIntensity, "\e[48;5;12m")]
    [InlineData(AnsiColor.Magenta | AnsiColor.HighIntensity, "\e[48;5;13m")]
    [InlineData(AnsiColor.Cyan | AnsiColor.HighIntensity, "\e[48;5;14m")]
    [InlineData(AnsiColor.White | AnsiColor.HighIntensity, "\e[48;5;15m")]
    public void SetBackgroundColor_WithTestCases(AnsiColor color, string expected)
    {
        var writer = new AnsiStringBuilder(new StringBuilder());
        string output = writer.SetBackgroundColor(color).Target.ToString();

        Assert.Equal(expected.Replace("\e", "\\e"), output.Replace("\e", "\\e"));
    }
}