namespace LasseVK.Console.Ansi;

/// <summary>
/// This enum represents the ANSI colors, used by
/// <see cref="AnsiWriter{T}.SetForegroundColor(AnsiColor)"/> and
/// <see cref="AnsiWriter{T}.SetBackgroundColor(AnsiColor)"/>.
/// </summary>
[Flags]
public enum AnsiColor
{
    /// <summary>
    /// Combine this flag with any other color to make it high intensity,
    /// or brighter.
    /// </summary>
    HighIntensity = 8,

    /// <summary>
    /// Black (#000000) or Gray (#808080 - high intensity).
    /// </summary>
    Black = 0,

    /// <summary>
    /// Red (#800000) or Bright Red (#FF0000 - high intensity).
    /// </summary>
    Red = 1,

    /// <summary>
    /// Green (#008000) or Bright Green (#00FF00 - high intensity).
    /// </summary>
    Green = 2,

    /// <summary>
    /// Yellow (#808000) or Bright Yellow (#FFFF00 - high intensity).
    /// </summary>
    Yellow = 3,

    /// <summary>
    /// Blue (#000080) or Bright Blue (#0000FF - high intensity).
    /// </summary>
    Blue = 4,

    /// <summary>
    /// Magenta (#800080) or Bright Magenta (#FF00FF - high intensity).
    /// </summary>
    Magenta = 5,

    /// <summary>
    /// Cyan (#008080) or Bright Cyan (#00FFFF - high intensity).
    /// </summary>
    Cyan = 6,

    /// <summary>
    /// White/Gray (#FFFFFF) or Bright White (#FFFFFF - high intensity).
    /// </summary>
    White = 7,
}