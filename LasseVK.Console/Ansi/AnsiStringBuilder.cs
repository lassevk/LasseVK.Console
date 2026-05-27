using System.Text;

namespace LasseVK.Console.Ansi;

public class AnsiStringBuilder : AnsiWriter<AnsiStringBuilder>
{
    private readonly StringBuilder _stringBuilder;

    public AnsiStringBuilder(StringBuilder stringBuilder)
    {
        _stringBuilder = stringBuilder ?? throw new ArgumentNullException(nameof(stringBuilder));
    }

    protected override void Write(string text)
    {
        _stringBuilder.Append(text);
    }

    protected override void Write(int value)
    {
        _stringBuilder.Append(value);
    }

    protected override void Write<TValue>(TValue value)
    {
        _stringBuilder.Append(value);
    }
}