using LasseVK.Console.Ansi;

namespace LasseVK.Console;

public class ConsoleLine
{
    private string _text;
    private readonly AnsiTextWriter _ansiWriter;
    private readonly TextWriter _writer;

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

    public void Clear() => Set("");

    public void Set(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        if (text == _text)
        {
            return;
        }

        int index = IndexOfFirstDifference(_text, text);
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
}