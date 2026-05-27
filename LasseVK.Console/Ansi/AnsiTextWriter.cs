namespace LasseVK.Console.Ansi;

public class AnsiTextWriter : AnsiWriter<AnsiTextWriter>
{
    private readonly TextWriter _textWriter;

    public AnsiTextWriter(TextWriter textWriter)
    {
        _textWriter = textWriter ?? throw new ArgumentNullException(nameof(textWriter));
    }

    protected override AnsiTextWriter Write(string text)
    {
        _textWriter.Write(text);
        return this;
    }

    protected override AnsiTextWriter Write(int value)
    {
        _textWriter.Write(value);
        return this;
    }

    protected override AnsiTextWriter Write<TValue>(TValue value)
    {
        _textWriter.Write(value);
        return this;
    }
}