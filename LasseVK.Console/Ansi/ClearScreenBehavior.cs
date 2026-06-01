namespace LasseVK.Console.Ansi;

/// <summary>
/// Used by <see cref="AnsiWriter{T}.ClearScreen(ClearScreenBehavior)"/>.
/// </summary>
public enum ClearScreenBehavior
{
    /// <summary>
    /// Clear from the cursor to the end of the screen.
    /// </summary>
    FromCursorToEndOfScreen,

    /// <summary>
    /// Clear from the cursor to the beginning of the screen.
    /// </summary>
    FromCursorToBeginningOfScreen,

    /// <summary>
    /// Clear the entire screen.
    /// </summary>
    EntireScreen,

    /// <summary>
    /// Clear the entire screen and the scrollback buffer.
    /// </summary>
    EntireScreenAndClearScrollbackBuffer,
}