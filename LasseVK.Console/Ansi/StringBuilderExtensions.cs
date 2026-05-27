using System.Text;

namespace LasseVK.Console.Ansi;

public static class StringBuilderExtensions
{
    public static AnsiStringBuilder WithAnsi(this StringBuilder stringBuilder) => new AnsiStringBuilder(stringBuilder);
}