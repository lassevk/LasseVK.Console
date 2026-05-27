namespace LasseVK.Console.Ansi;

public static class TextWriterExtensions
{
    public static AnsiTextWriter WithAnsi(this TextWriter writer) => new AnsiTextWriter(writer);
}