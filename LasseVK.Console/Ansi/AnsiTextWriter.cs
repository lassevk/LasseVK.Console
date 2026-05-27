namespace LasseVK.Console.Ansi;

public class AnsiTextWriter : AnsiWriter<AnsiTextWriter>
{
    private readonly TextWriter _textWriter;

    public AnsiTextWriter(TextWriter textWriter)
    {
        _textWriter = textWriter ?? throw new ArgumentNullException(nameof(textWriter));
    }

    protected override void Write(string text)
    {
        _textWriter.Write(text);
    }

    protected override void Write(int value)
    {
        _textWriter.Write(value);
    }

    protected override void Write<TValue>(TValue value)
    {
        _textWriter.Write(value);
    }
}