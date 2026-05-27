using LasseVK.Console.Ansi;

namespace LasseVK.Console;

public class ConsoleLines : IDisposable
{
    private readonly AnsiTextWriter _ansiWriter;
    private readonly TextWriter _writer;
    private readonly List<ConsoleLine> _lines = [];

    public ConsoleLines(int count, TextWriter? writer = null)
    {
        _writer = writer ?? System.Console.Out;
        _ansiWriter = _writer.WithAnsi();
        ArgumentOutOfRangeException.ThrowIfLessThan(count, 1);

        for (int i = 0; i < count; i++)
        {
            _lines.Add(new ConsoleLine());
            _writer.WriteLine();
        }

        _lines = Enumerable.Range(0, count).Select(i => new ConsoleLine()).ToList();
    }

    public ConsoleLines(TextWriter writer, params string[] lines)
    {
        _writer = writer;
        _ansiWriter = _writer.WithAnsi();

        foreach (string line in lines)
        {
            _lines.Add(new ConsoleLine(line));
            _writer.WriteLine();
        }
    }

    public ConsoleLines(params string[] lines)
        : this(System.Console.Out, lines)
    {
    }

    public void Set(int index, string text)
    {
        _ansiWriter.MoveUp(_lines.Count - index);
        _lines[index].Set(text);
        _ansiWriter.MoveBeginningOfLinesDown(_lines.Count - index);
    }

    public void Remove(int index)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(index, _lines.Count - 1);

        _ansiWriter.MoveUp(_lines.Count - index);
        while (index < _lines.Count - 1)
        {
            _lines[index].Set(_lines[index + 1].Text);
            _ansiWriter.MoveBeginningOfLinesDown();
            index++;
        }

        _lines.Last().Dispose();
        _lines.RemoveAt(_lines.Count - 1);
    }

    public void Insert(int index, string text = "")
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(index, _lines.Count);

        _lines.Add(new ConsoleLine());
        _writer.WriteLine();

        int lastIndex = _lines.Count - 1;
        while (lastIndex > index)
        {
            Set(lastIndex, _lines[lastIndex - 1].Text);
            lastIndex--;
        }

        Set(index, text);
    }

    public void Clear()
    {
        while (_lines.Count > 0)
        {
            Remove(_lines.Count - 1);
        }
    }

    public void Dispose()
    {
        Clear();
    }
}