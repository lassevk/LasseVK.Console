namespace LasseVK.Console.Ansi;

public enum ClearScreenBehavior
{
    FromCursorToEndOfScreen,
    FromCursorToBeginningOfScreen,
    EntireScreen,
    EntireScreenAndClearScrollbackBuffer,
}