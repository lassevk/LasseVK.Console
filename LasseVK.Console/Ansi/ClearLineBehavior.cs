namespace LasseVK.Console.Ansi;

/// <summary>
/// Used by <see cref="AnsiWriter{T}.ClearLine(ClearLineBehavior)"/>.
/// </summary>
public enum ClearLineBehavior
{
    /// <summary>
    /// Clear from the cursor to the end of the line.
    /// </summary>
    FromCursorToEndOfLine,

    /// <summary>
    /// Clear from the cursor to the beginning of the line.
    /// </summary>
    FromCursorToBeginningOfLine,

    /// <summary>
    /// Clear the entire line.
    /// </summary>
    EntireLine,
}