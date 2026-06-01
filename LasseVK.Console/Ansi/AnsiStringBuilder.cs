using System.Text;

namespace LasseVK.Console.Ansi;

/// <summary>
/// Implementation of <see cref="AnsiWriter{T}"/> that writes to a <see cref="StringBuilder"/>.
/// </summary>
public class AnsiStringBuilder : AnsiWriter<AnsiStringBuilder>
{
    private readonly StringBuilder _stringBuilder;

    /// <summary>
    /// Creates a new instance of <see cref="AnsiStringBuilder"/>.
    /// </summary>
    /// <param name="stringBuilder">
    /// The <see cref="StringBuilder"/> to write to.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="stringBuilder"/> is <see langword="null"/>.
    /// </exception>
    public AnsiStringBuilder(StringBuilder stringBuilder)
    {
        _stringBuilder = stringBuilder ?? throw new ArgumentNullException(nameof(stringBuilder));
    }

    /// <inheritdoc/>
    protected override AnsiStringBuilder Write(string text)
    {
        _stringBuilder.Append(text);
        return this;
    }

    /// <inheritdoc/>
    protected override AnsiStringBuilder Write(int value)
    {
        _stringBuilder.Append(value);
        return this;
    }

    /// <inheritdoc/>
    protected override AnsiStringBuilder Write<TValue>(TValue value)
    {
        _stringBuilder.Append(value);
        return this;
    }
}