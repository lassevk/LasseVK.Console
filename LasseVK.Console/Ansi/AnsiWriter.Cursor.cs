namespace LasseVK.Console.Ansi;

public partial class AnsiWriter<T>
{
    /// <summary>
    /// Moves the cursor up the specified amount of rows.
    /// </summary>
    /// <param name="amount">
    /// The amount to move the cursor up.
    /// </param>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="amount"/> is less than 0.
    /// </exception>
    public T MoveUp(int amount = 1)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(amount, 0);

        return amount switch
        {
            0 => (T)this
          , 1 => Write("\e[A")
          , _ => Write($"\e[{amount}A")
           ,
        };
    }

    /// <summary>
    /// Moves the cursor down the specified amount of rows.
    /// </summary>
    /// <param name="amount">
    /// The amount to move the cursor down.
    /// </param>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="amount"/> is less than 0.
    /// </exception>
    public T MoveDown(int amount = 1)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(amount, 0);

        return amount switch
        {
            0 => (T)this
          , 1 => Write("\e[B")
          , _ => Write($"\e[{amount}B"),
        };
    }

    /// <summary>
    /// Moves the cursor left the specified amount of columns.
    /// </summary>
    /// <param name="amount">
    /// The amount to move the cursor left.
    /// </param>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="amount"/> is less than 0.
    /// </exception>
    public T MoveLeft(int amount = 1)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(amount, 0);

        return amount switch
        {
            0 => (T)this
          , 1 => Write("\e[D")
          , _ => Write($"\e[{amount}D"),
        };
    }

    /// <summary>
    /// Moves the cursor right the specified amount of columns.
    /// </summary>
    /// <param name="amount">
    /// The amount to move the cursor right.
    /// </param>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="amount"/> is less than 0.
    /// </exception>
    public T MoveRight(int amount = 1)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(amount, 0);

        return amount switch
        {
            0 => (T)this
          , 1 => Write("\e[C")
          , _ => Write($"\e[{amount}C"),
        };
    }

    /// <summary>
    /// Moves the cursor down the specified amount of lines, and moves it to the start of that
    /// line.
    /// </summary>
    /// <remarks>
    /// If <paramref name="amount"/> is 0, this is equivalent to <see cref="MoveToColumn(int)"/> moving to column 1.
    /// </remarks>
    /// <param name="amount">
    /// The number of lines to move down.
    /// </param>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="amount"/> is less than 0.
    /// </exception>
    public T MoveBeginningOfLinesDown(int amount = 1)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(amount, 0);

        return amount switch
        {
            0 => MoveToColumn(1)
          , 1 => Write("\e[E")
          , _ => Write($"\e[{amount}E"),
        };
    }

    /// <summary>
    /// Moves the cursor up the specified amount of lines, and moves it to the start of that
    /// line.
    /// </summary>
    /// <remarks>
    /// If <paramref name="amount"/> is 0, this is equivalent to <see cref="MoveToColumn(int)"/> moving to column 1.
    /// </remarks>
    /// <param name="amount">
    /// The number of lines to move up.
    /// </param>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="amount"/> is less than 0.
    /// </exception>
    public T MoveBeginningOfLinesUp(int amount = 1)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(amount, 0);

        return amount switch
        {
            0 => MoveToColumn(1)
          , 1 => Write("\e[F")
          , _ => Write($"\e[{amount}F"),
        };
    }

    /// <summary>
    /// Moves the cursor to the specified column.
    /// </summary>
    /// <param name="column">
    /// The column to move to, 1-based.
    /// </param>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="column"/> is less than 1.
    /// </exception>
    public T MoveToColumn(int column = 1)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(column, 1);

        return column switch
        {
            1 => Write("\e[G")
          , _ => Write($"\e[{column}G"),
        };
    }

    /// <summary>
    /// Moves the cursor to the top left corner of the screen.
    /// </summary>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    public T MoveToTopLeft() => MoveTo();

    /// <summary>
    /// Moves the cursor to the specified position.
    /// </summary>
    /// <param name="column">
    /// The column to move to, 1-based.
    /// </param>
    /// <param name="row">
    /// The row to move to, 1-based.
    /// </param>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="column"/> or <paramref name="row"/> is less than 1.
    /// </exception>
    public T MoveTo(int column = 1, int row = 1)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(column, 1);
        ArgumentOutOfRangeException.ThrowIfLessThan(row, 1);

        return column switch
        {
            1 when row == 1 => Write($"\e[;H")
          , 1               => Write($"\e[{row};H")
          , _               => row == 1 ? Write($"\e[;{column}H") : Write($"\e[{row};{column}H"),
        };
    }

    /// <summary>
    /// Clears the screen, starting at the current cursor position and clearing the
    /// the rest of the screen, meaning the rest of the current line, and all lines below.
    /// </summary>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    public T ClearToEndOfScreen() => ClearScreen(ClearScreenBehavior.FromCursorToEndOfScreen);

    /// <summary>
    /// Clears the screen, starting at the current cursor position and clearing to the
    /// top left corner of the screen, meaning the start of the current line, and all lines above.
    /// </summary>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    public T ClearToBeginningOfScreen() => ClearScreen(ClearScreenBehavior.FromCursorToBeginningOfScreen);

    /// <summary>
    /// Clears the entire screen and moves the cursor to the top left corner.
    /// </summary>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    public T ClearEntireScreen() => ClearScreen(ClearScreenBehavior.EntireScreen).MoveTo();

    /// <summary>
    /// Clears the entire screen and moves the cursor to the top left corner,
    /// and clears the scrollback buffer.
    /// </summary>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    public T ClearEntireScreenAndClearScrollbackBuffer() => ClearScreen(ClearScreenBehavior.EntireScreenAndClearScrollbackBuffer);

    /// <summary>
    /// Clears the screen based on the given behavior.
    /// </summary>
    /// <param name="behavior">
    /// The <see cref="ClearScreenBehavior"/> to use.
    /// </param>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="behavior"/> is not a valid <see cref="ClearScreenBehavior"/>.
    /// </exception>
    public T ClearScreen(ClearScreenBehavior behavior)
    {
        return behavior switch
        {
            ClearScreenBehavior.FromCursorToEndOfScreen              => Write("\e[0J")
          , ClearScreenBehavior.FromCursorToBeginningOfScreen        => Write("\e[1J")
          , ClearScreenBehavior.EntireScreen                         => Write("\e[2J")
          , ClearScreenBehavior.EntireScreenAndClearScrollbackBuffer => Write("\e[3J")
          , _                                                        => throw new ArgumentOutOfRangeException(nameof(behavior), behavior, null),
        };
    }

    /// <summary>
    /// Clears from the cursor to the end of the current line.
    /// </summary>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    public T ClearToEndOfLine() => ClearLine(ClearLineBehavior.FromCursorToEndOfLine);

    /// <summary>
    /// Clears from the cursor to the beginning of the current line.
    /// </summary>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    public T ClearToBeginningOfLine() => ClearLine(ClearLineBehavior.FromCursorToBeginningOfLine);

    /// <summary>
    /// Clears the entire current line and moves the cursor to the beginning of it.
    /// </summary>
    /// <returns></returns>
    public T ClearEntireLine() => ClearLine(ClearLineBehavior.EntireLine).MoveToColumn();

    /// <summary>
    /// Clears the current line based on the given behavior.
    /// </summary>
    /// <param name="behavior">
    /// The <see cref="ClearLineBehavior"/> to use.
    /// </param>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="behavior"/> is not a valid <see cref="ClearLineBehavior"/>.
    /// </exception>
    public T ClearLine(ClearLineBehavior behavior)
    {
        return behavior switch
        {
            ClearLineBehavior.FromCursorToEndOfLine       => Write("\e[0K")
          , ClearLineBehavior.FromCursorToBeginningOfLine => Write("\e[1K")
          , ClearLineBehavior.EntireLine                  => Write("\e[2K")
          , _                                             => throw new ArgumentOutOfRangeException(nameof(behavior), behavior, null),
        };
    }

    /// <summary>
    /// Scrolls the screen up by the specified amount of lines.
    /// </summary>
    /// <param name="amount">
    /// The amount to scroll up.
    /// </param>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="amount"/> is less than 0.
    /// </exception>
    public T ScrollUp(int amount = 1)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(amount, 0);

        return amount switch
        {
            0 => (T)this
          , 1 => Write("\e[S")
          , _ => Write($"\e[{amount}S"),
        };
    }

    /// <summary>
    /// Scrolls the screen down by the specified amount of lines.
    /// </summary>
    /// <param name="amount">
    /// The amount to scroll down.
    /// </param>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="amount"/> is less than 0.
    /// </exception>
    public T ScrollDown(int amount = 1)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(amount, 0);

        return amount switch
        {
            0 => (T)this
          , 1 => Write("\e[T")
          , _ => Write($"\e[{amount}T"),
        };
    }

    /// <summary>
    /// Saves the current cursor position.
    /// </summary>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    public T SaveCursorPosition() => Write("\e[s");

    /// <summary>
    /// Restores the cursor position to the one that was saved with <see cref="SaveCursorPosition"/>.
    /// </summary>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    public T RestoreCursorPosition() => Write("\e[u");

    /// <summary>
    /// Shows the cursor.
    /// </summary>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    public T ShowCursor() => Write("\e[?25h");

    /// <summary>
    /// Hides the cursor.
    /// </summary>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    public T HideCursor() => Write("\e[?25l");
}