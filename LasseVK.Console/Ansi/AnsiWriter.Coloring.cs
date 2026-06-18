namespace LasseVK.Console.Ansi;

public partial class AnsiWriter<T>
    where T : AnsiWriter<T>
{
    /// <summary>
    /// Changes the foreground color of text to be written.
    /// </summary>
    /// <param name="color">
    /// One of the <see cref="AnsiColor"/> values.
    /// </param>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    public T SetForegroundColor(AnsiColor color) => Write($"\e[38;5;{(int)color}m");

    /// <summary>
    /// Changes the background color of text to be written.
    /// </summary>
    /// <param name="color">
    /// One of the <see cref="AnsiColor"/> values.
    /// </param>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    public T SetBackgroundColor(AnsiColor color) => Write($"\e[48;5;{(int)color}m");
}