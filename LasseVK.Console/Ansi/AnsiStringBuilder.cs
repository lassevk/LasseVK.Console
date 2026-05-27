using System.Text;

namespace LasseVK.Console.Ansi;

public class AnsiStringBuilder : AnsiWriter<AnsiStringBuilder>
{
    private readonly StringBuilder _stringBuilder;

    public AnsiStringBuilder(StringBuilder stringBuilder)
    {
        _stringBuilder = stringBuilder ?? throw new ArgumentNullException(nameof(stringBuilder));
    }

    protected override AnsiStringBuilder Write(string text)
    {
        _stringBuilder.Append(text);
        return this;
    }

    protected override AnsiStringBuilder Write(int value)
    {
        _stringBuilder.Append(value);
        return this;
    }

    protected override AnsiStringBuilder Write<TValue>(TValue value)
    {
        _stringBuilder.Append(value);
        return this;
    }
}