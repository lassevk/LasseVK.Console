namespace LasseVK.Console.Ansi;

/// <summary>
/// Implementation of <see cref="AnsiWriter{T}"/> that writes to a <see cref="Target"/>.
/// </summary>
public class AnsiTextWriter : AnsiWriter<AnsiTextWriter>
{
    /// <summary>
    /// Creates a new instance of <see cref="AnsiTextWriter"/>.
    /// </summary>
    /// <param name="textWriter">
    /// The <see cref="Target"/> to write to.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="textWriter"/> is <see langword="null"/>.
    /// </exception>
    public AnsiTextWriter(TextWriter textWriter)
    {
        Target = textWriter ?? throw new ArgumentNullException(nameof(textWriter));
    }

    /// <summary>
    /// Gets the <see cref="Target"/> that this instance writes to.
    /// </summary>
    public TextWriter Target { get; }

    /// <inheritdoc/>
    protected override AnsiTextWriter Write(string text)
    {
        Target.Write(text);
        return this;
    }

    /// <inheritdoc/>
    protected override AnsiTextWriter Write(int value)
    {
        Target.Write(value);
        return this;
    }

    /// <inheritdoc/>
    protected override AnsiTextWriter Write<TValue>(TValue value)
    {
        Target.Write(value);
        return this;
    }
}