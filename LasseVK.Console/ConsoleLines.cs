using LasseVK.Console.Ansi;

namespace LasseVK.Console;

/// <summary>
/// This class represents a collection of console lines that can be managed and displayed.
/// </summary>
public class ConsoleLines : IDisposable
{
    private readonly AnsiTextWriter _ansiWriter;
    private readonly TextWriter _writer;
    private readonly List<ConsoleLine> _lines = [];

    /// <summary>
    /// Creates a new collection of console lines, all starting out as empty.
    /// </summary>
    /// <param name="count">
    /// The number of lines to create.
    /// </param>
    /// <param name="writer">
    /// The writer to use for writing to the console, defaults to <see cref="System.Console.Out"/>.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="count"/> is less than 1.
    /// </exception>
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

        _lines = Enumerable.Range(0, count).Select(_ => new ConsoleLine()).ToList();
    }

    /// <summary>
    /// Creates a new collection of console lines, all starting out with the given text.
    /// </summary>
    /// <param name="writer">
    /// The writer to use for writing to the console.
    /// </param>
    /// <param name="lines">
    /// The text to write to the console.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="writer"/> is <see langword="null"/>.
    /// <para>-or-</para>
    /// <paramref name="lines"/> is <see langword="null"/>.
    /// <para>-or-</para>
    /// <paramref name="lines"/> contains <see langword="null"/>.
    /// </exception>
    public ConsoleLines(TextWriter writer, params string[] lines)
    {
        ArgumentNullException.ThrowIfNull(writer);
        ArgumentNullException.ThrowIfNull(lines);

        _writer = writer;
        _ansiWriter = _writer.WithAnsi();

        foreach (string line in lines)
        {
            if (line == null)
            {
                throw new ArgumentNullException(nameof(lines));
            }

            _lines.Add(new ConsoleLine(line));
            _writer.WriteLine();
        }
    }

    /// <summary>
    /// Creates a new collection of console lines, all starting out with the given text.
    /// The lines will be written to <see cref="System.Console.Out"/>.
    /// </summary>
    /// <param name="lines">
    /// The text to write to the console.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="lines"/> is <see langword="null"/>.
    /// <para>-or-</para>
    /// <paramref name="lines"/> contains <see langword="null"/>.
    /// </exception>
    public ConsoleLines(params string[] lines)
        : this(System.Console.Out, lines)
    {
    }

    /// <summary>
    /// Gets the number of lines in the collection.
    /// </summary>
    public int Count => _lines.Count;

    /// <summary>
    /// Gets or sets the text of the line at the given index.
    /// </summary>
    /// <param name="index">
    /// The 0-based index of the line to get or set.
    /// </param>
    public string this[int index]
    {
        get => _lines[index].Text;
        set => Set(index, value ?? "");
    }

    private void Set(int index, string text)
    {
        _ansiWriter.HideCursor();
        try
        {
            _ansiWriter.MoveUp(_lines.Count - index);
            try
            {
                _lines[index].Text = text;
            }
            finally
            {
                _ansiWriter.MoveBeginningOfLinesDown(_lines.Count - index);
            }
        }
        finally
        {
            _ansiWriter.ShowCursor();
        }
    }

    /// <summary>
    /// Removes the line at the given index, moving the lines below up one line.
    /// </summary>
    /// <param name="index">
    /// The 0-based index of the line to remove.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="index"/> is less than 0 or greater than or equal to <see cref="Count"/>.
    /// </exception>
    public void Remove(int index)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(index, 0);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(index, _lines.Count - 1);

        _ansiWriter.MoveUp(_lines.Count - index);
        while (index < _lines.Count - 1)
        {
            _lines[index].Text = _lines[index + 1].Text;
            _ansiWriter.MoveBeginningOfLinesDown();
            index++;
        }

        _lines.Last().Dispose();
        _lines.RemoveAt(_lines.Count - 1);
    }

    /// <summary>
    /// Inserts a new line at the given index, moving the lines below down one line.
    /// </summary>
    /// <param name="index">
    /// The 0-based index of the line to insert at.
    /// </param>
    /// <param name="text">
    /// The text to insert.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="index"/> is less than 0 or greater than or equal to <see cref="Count"/>.
    /// </exception>
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

        Set(index, text ?? "");
    }

    /// <summary>
    /// Clears all lines in the collection, similar to calling <see cref="Remove(int)"/> for each line in the collection.
    /// </summary>
    public void Clear()
    {
        _ansiWriter.HideCursor();
        try
        {
            for (int index = _lines.Count - 1; index >= 0; index--)
            {
                _ansiWriter.MoveBeginningOfLinesUp();
                _lines[index].Dispose();
                _lines.RemoveAt(index);
            }
        }
        finally
        {
            _ansiWriter.ShowCursor();
        }
    }

    /// <summary>
    /// Disposes of the <see cref="ConsoleLines"/> instance, clearing all lines and releasing resources.
    /// </summary>
    public void Dispose()
    {
        Clear();
    }

    /// <summary>
    /// Scrolls up all lines in the collection and appends the given line to the bottom.
    /// The first line will be removed.
    /// </summary>
    /// <param name="line">
    /// The line to append to the bottom.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="line"/> is <see langword="null"/>.
    /// </exception>
    public void ScrollUpAndAppend(string line)
    {
        ArgumentNullException.ThrowIfNull(line);

        for (int index = 0; index < _lines.Count - 1; index++)
        {
            Set(index, _lines[index + 1].Text);
        }

        Set(_lines.Count - 1, line);
    }
}