namespace LasseVK.Console.Ansi;

/// <summary>
/// Provides extension methods for <see cref="TextWriter"/>.
/// </summary>
public static class TextWriterExtensions
{
    /// <summary>
    /// Creates a new <see cref="AnsiTextWriter"/> that writes to the specified <see cref="TextWriter"/>.
    /// </summary>
    /// <param name="writer">
    /// The <see cref="TextWriter"/> to write to.
    /// </param>
    /// <returns>
    /// A new <see cref="AnsiTextWriter"/> that writes to the specified <see cref="TextWriter"/>.
    /// </returns>
    public static AnsiTextWriter WithAnsi(this TextWriter writer) => new AnsiTextWriter(writer);
}