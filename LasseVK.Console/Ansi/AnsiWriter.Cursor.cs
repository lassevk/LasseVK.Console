namespace LasseVK.Console.Ansi;

public partial class AnsiWriter<T>
{
    public T MoveUp(int amount = 1)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(amount, 0);

        switch (amount)
        {
            case 0:
                return (T)this;

            case 1:
                Write("\e[A");
                return (T)this;

            default:
                Write($"\e[{amount}A");
                return (T)this;
        }
    }

    public T MoveDown(int amount = 1)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(amount, 0);

        return amount switch
        {
            0 => (T)this
          , 1 => Write("\e[b")
          , _ => Write($"\e[{amount}B"),
        };
    }

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

    public T MoveToColumn(int column = 1)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(column, 1);

        return column switch
        {
            1 => Write("\e[G")
          , _ => Write($"\e[{column}G")
        };
    }

    public T MoveToTopLeft() => MoveTo();

    public T MoveTo(int column = 1, int row = 1)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(column, 1);
        ArgumentOutOfRangeException.ThrowIfLessThan(row, 1);

        return column switch
        {
            1 when row == 1 => Write($"\e[;H")
          , 1               => Write($"\e[{row};H")
          , _               => row == 1 ? Write($"\e[;{column}H") : Write($"\e[{row};{column}H")
        };
    }

    public T ClearToEndOfScreen() => ClearScreen(ClearScreenBehavior.FromCursorToEndOfScreen);
    public T ClearToBeginningOfScreen() => ClearScreen(ClearScreenBehavior.FromCursorToBeginningOfScreen);
    public T ClearEntireScreen() => ClearScreen(ClearScreenBehavior.EntireScreen).MoveTo();

    public T ClearEntireScreenAndClearScrollbackBuffer() => ClearScreen(ClearScreenBehavior.EntireScreenAndClearScrollbackBuffer);

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

    public T ClearToEndOfLine() => ClearLine(ClearLineBehavior.FromCursorToEndOfLine);
    public T ClearToBeginningOfLine() => ClearLine(ClearLineBehavior.FromCursorToBeginningOfLine);
    public T ClearEntireLine() => ClearLine(ClearLineBehavior.EntireLine);

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

    public T SaveCursorPosition() => Write("\e[s");
    public T RestoreCursorPosition() => Write("\e[u");

    public T ShowCursor() => Write("\e[?25h");
    public T HideCursor() => Write("\e[?25l");
}