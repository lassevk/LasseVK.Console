using LasseVK.Console.Ansi;

namespace LasseVK.Console;

public class ConsoleLine : IDisposable
{
    private readonly AnsiTextWriter _ansiWriter;
    private readonly TextWriter _writer;

    public ConsoleLine(string text = "", TextWriter? writer = null)
    {
        ArgumentNullException.ThrowIfNull(text);

        _writer = writer ?? System.Console.Out;
        _ansiWriter = _writer.WithAnsi();
        Text = text;
        _ansiWriter.MoveToColumn();
        if (text != "")
        {
            _writer.Write(text);
        }
        _ansiWriter.ClearToEndOfLine();
    }

    public string Text { get; private set; }

    public void Clear() => Set("");

    public void Set(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        if (text == Text)
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
        Text = text;
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

    public void Dispose()
    {
        Set("");
    }
}