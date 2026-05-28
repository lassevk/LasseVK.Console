using LasseVK.Console.Ansi;

namespace LasseVK.Console;

/// <summary>
/// This class represents a line of text in the console, providing methods for manipulating and displaying text, doing
/// optimal replacement of the actual text on screen.
/// </summary>
public class ConsoleLine : IDisposable
{
    private readonly AnsiTextWriter _ansiWriter;
    private readonly TextWriter _writer;

    private string _text;

    /// <summary>
    /// Constructs a new instance of the <see cref="ConsoleLine"/> class.
    /// </summary>
    /// <param name="text">
    /// The initial text to display on the line.
    /// </param>
    /// <param name="writer">
    /// The <see cref="TextWriter"/> to use for writing text to the console. Defaults
    /// to <see cref="System.Console.Out"/>.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="text"/> is <see langword="null"/>.
    /// </exception>
    public ConsoleLine(string text = "", TextWriter? writer = null)
    {
        ArgumentNullException.ThrowIfNull(text);

        _writer = writer ?? System.Console.Out;
        _ansiWriter = _writer.WithAnsi();
        _text = text;
        _ansiWriter.MoveToColumn();
        if (text != "")
        {
            _writer.Write(text);
        }
        _ansiWriter.ClearToEndOfLine();
    }

    /// <summary>
    /// Gets or sets the text displayed on the line. This will immediately update the on-screen
    /// representation of the line.
    /// </summary>
    public string Text
    {
        get => _text;
        set => Set(value);
    }

    /// <summary>
    /// Clears the text displayed on the line. Similar to setting <see cref="Text"/> to an empty string.
    /// </summary>
    public void Clear() => Set("");

    private void Set(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        if (text.Length >= System.Console.WindowWidth)
        {
            text = text[..(System.Console.WindowWidth - 1)];
        }

        if (text == _text)
        {
            return;
        }

        int index = IndexOfFirstDifference(Text, text);
        _ansiWriter.HideCursor();
        if (index == 0)
        {
            _ansiWriter.MoveToColumn();
            _writer.Write(text);
        }
        else
        {
            _ansiWriter.MoveToColumn(index + 1);
            _writer.Write(text[index..]);
        }
        _ansiWriter.ClearToEndOfLine();
        _ansiWriter.ShowCursor();
        _text = text;
    }

    private int IndexOfFirstDifference(string text1, string text2)
    {
        int length = Math.Min(text1.Length, text2.Length);
        for (int index = 0; index < length; index++)
        {
            if (text1[index] != text2[index])
            {
                return index;
            }
        }
        return length;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Set("");
    }
}