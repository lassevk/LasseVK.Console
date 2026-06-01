using System.Text;

namespace LasseVK.Console.Ansi;

/// <summary>
/// Implementation of <see cref="AnsiWriter{T}"/> that writes to a <see cref="Target"/>.
/// </summary>
public class AnsiStringBuilder : AnsiWriter<AnsiStringBuilder>
{
    /// <summary>
    /// Creates a new instance of <see cref="AnsiStringBuilder"/>.
    /// </summary>
    /// <param name="stringBuilder">
    /// The <see cref="Target"/> to write to.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="stringBuilder"/> is <see langword="null"/>.
    /// </exception>
    public AnsiStringBuilder(StringBuilder stringBuilder)
    {
        Target = stringBuilder ?? throw new ArgumentNullException(nameof(stringBuilder));
    }

    /// <summary>
    /// Gets the <see cref="Target"/> that this instance writes to.
    /// </summary>
    public StringBuilder Target { get; }

    /// <inheritdoc/>
    protected override AnsiStringBuilder Write(string text)
    {
        Target.Append(text);
        return this;
    }

    /// <inheritdoc/>
    protected override AnsiStringBuilder Write(int value)
    {
        Target.Append(value);
        return this;
    }

    /// <inheritdoc/>
    protected override AnsiStringBuilder Write<TValue>(TValue value)
    {
        Target.Append(value);
        return this;
    }
}