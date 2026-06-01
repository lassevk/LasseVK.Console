using System.Text;

namespace LasseVK.Console.Ansi;

/// <summary>
/// Provides extension methods for <see cref="StringBuilder"/>.
/// </summary>
public static class StringBuilderExtensions
{
    /// <summary>
    /// Creates a new <see cref="AnsiStringBuilder"/> that writes to the specified <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="stringBuilder">
    /// The <see cref="StringBuilder"/> to write to.
    /// </param>
    /// <returns>
    /// A new <see cref="AnsiStringBuilder"/> that writes to the specified <see cref="StringBuilder"/>.
    /// </returns>
    public static AnsiStringBuilder WithAnsi(this StringBuilder stringBuilder) => new AnsiStringBuilder(stringBuilder);
}