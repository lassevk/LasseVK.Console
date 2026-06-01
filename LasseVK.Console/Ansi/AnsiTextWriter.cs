namespace LasseVK.Console.Ansi;

/// <summary>
/// Implementation of <see cref="AnsiWriter{T}"/> that writes to a <see cref="TextWriter"/>.
/// </summary>
public class AnsiTextWriter : AnsiWriter<AnsiTextWriter>
{
    private readonly TextWriter _textWriter;

    /// <summary>
    /// Creates a new instance of <see cref="AnsiTextWriter"/>.
    /// </summary>
    /// <param name="textWriter">
    /// The <see cref="TextWriter"/> to write to.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="textWriter"/> is <see langword="null"/>.
    /// </exception>
    public AnsiTextWriter(TextWriter textWriter)
    {
        _textWriter = textWriter ?? throw new ArgumentNullException(nameof(textWriter));
    }

    /// <inheritdoc/>
    protected override AnsiTextWriter Write(string text)
    {
        _textWriter.Write(text);
        return this;
    }

    /// <inheritdoc/>
    protected override AnsiTextWriter Write(int value)
    {
        _textWriter.Write(value);
        return this;
    }

    /// <inheritdoc/>
    protected override AnsiTextWriter Write<TValue>(TValue value)
    {
        _textWriter.Write(value);
        return this;
    }
}