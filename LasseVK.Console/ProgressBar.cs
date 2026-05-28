using System.Globalization;
using System.Text;

namespace LasseVK.Console;

/// <summary>
/// This class provides static methods for formatting progress bars.
/// </summary>
public static class ProgressBar
{
    /// <summary>
    /// This is the length of the progress bar. If you provide a character buffer,
    /// it must be at least this length.
    /// </summary>
    public const int Length = 34;

    private static readonly char[] _blocks = [' ', '\u258f', '\u258e', '\u258d', '\u258c', '\u258b', '\u258a', '\u2589', '\u2588'];

    /// <summary>
    /// Formats the progress bar to the specified buffer.
    /// </summary>
    /// <param name="target">
    /// The buffer to write the progress bar to. This must be at least <see cref="Length"/> characters long.
    /// </param>
    /// <param name="progress">
    /// The current progress of the process or what the progress bar represents.
    /// Range from 0 to <paramref name="total"/>, inclusive.
    /// </param>
    /// <param name="total">
    /// The total amount of progress that can be made.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// The buffer is too small.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="progress"/> or <paramref name="total"/> is less than zero.
    /// Thrown if <paramref name="progress"/> is greater than <paramref name="total"/>.
    /// </exception>
    public static void FormatTo(Span<char> target, int progress, int total)
    {
        if (target.Length < Length)
        {
            throw new InvalidOperationException("Target buffer is too small");
        }
        ArgumentOutOfRangeException.ThrowIfLessThan(progress, 0);
        ArgumentOutOfRangeException.ThrowIfLessThan(total, 0);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(progress, total, "Progress cannot be greater than total");

        decimal percent = progress * 100.0M / total;
        decimal blockCount = percent / 4M;

        int whole = (int)Math.Floor(blockCount);
        int fraction = (int)Math.Floor((blockCount - Math.Floor(blockCount)) * 8);

        // result = "[                         ] 100.0%"
        // result = "[                         ]  50.0%"
        // result = "[                         ]   5.0%"
        //                     1         2         3
        //           0123456789012345678901234567890123
        target[0] = '[';
        target[26] = ']';
        target[27] = ' ';
        target[33] = '%';
        int padding = percent switch
        {
            < 10.0M  => 2,
            < 100.0M => 1,
            _        => 0,
        };

        target[28] = ' ';
        target[29] = ' ';
        percent.TryFormat(target[(28 + padding)..], out int _, "0.0", CultureInfo.InvariantCulture);

        for (int index = 0; index < whole; index++)
        {
            target[index + 1] = '\u2588';
        }

        if (fraction != 0)
        {
            target[whole + 1] = _blocks[fraction];
            for (int index = whole + 1; index < 25; index++)
            {
                target[index + 1] = ' ';
            }
        }
        else
        {
            for (int index = whole; index < 25; index++)
            {
                target[index + 1] = ' ';
            }
        }

    }

    /// <summary>
    /// Formats the progress bar and returns it as a string.
    /// </summary>
    /// <param name="progress">
    /// The current progress of the process or what the progress bar represents.
    /// Range from 0 to <paramref name="total"/>, inclusive.
    /// </param>
    /// <param name="total">
    /// The total amount of progress that can be made.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="progress"/> or <paramref name="total"/> is less than zero.
    /// Thrown if <paramref name="progress"/> is greater than <paramref name="total"/>.
    /// </exception>
    public static string Format(int progress, int total)
    {
        Span<char> buffer = stackalloc char[Length];
        FormatTo(buffer, progress, total);
        return buffer.ToString();
    }

    /// <summary>
    /// Formats the progress bar to the specified <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="target">
    /// The buffer to write the progress bar to. This must be at least <see cref="Length"/> characters long.
    /// </param>
    /// <param name="progress">
    /// The current progress of the process or what the progress bar represents.
    /// Range from 0 to <paramref name="total"/>, inclusive.
    /// </param>
    /// <param name="total">
    /// The total amount of progress that can be made.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="target"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="progress"/> or <paramref name="total"/> is less than zero.
    /// Thrown if <paramref name="progress"/> is greater than <paramref name="total"/>.
    /// </exception>
    public static void FormatTo(StringBuilder target, int progress, int total)
    {
        ArgumentNullException.ThrowIfNull(target);

        Span<char> buffer = stackalloc char[Length];
        FormatTo(buffer, progress, total);
        target.Append(buffer);
    }

    /// <summary>
    /// Formats the progress bar and writes it to the specified <see cref="TextWriter"/>.
    /// </summary>
    /// <param name="target">
    /// The buffer to write the progress bar to. This must be at least <see cref="Length"/> characters long.
    /// </param>
    /// <param name="progress">
    /// The current progress of the process or what the progress bar represents.
    /// Range from 0 to <paramref name="total"/>, inclusive.
    /// </param>
    /// <param name="total">
    /// The total amount of progress that can be made.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="target"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="progress"/> or <paramref name="total"/> is less than zero.
    /// Thrown if <paramref name="progress"/> is greater than <paramref name="total"/>.
    /// </exception>
    public static void FormatTo(TextWriter target, int progress, int total)
    {
        ArgumentNullException.ThrowIfNull(target);

        Span<char> buffer = stackalloc char[Length];
        FormatTo(buffer, progress, total);
        target.Write(buffer);
    }
}