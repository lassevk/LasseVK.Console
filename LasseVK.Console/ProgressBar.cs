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

    static ProgressBar()
    {
        System.Console.OutputEncoding = Encoding.UTF8;
    }

    // ReSharper disable InconsistentNaming
    private const char RIGHT_ONE_EIGHTH_BLOCK = '\u2595';

    private const char LEFT_ONE_EIGHTH_BLOCK = '\u258f';
    private const char LEFT_ONE_QUARTER_BLOCK = '\u258e';
    private const char LEFT_THREE_EIGHTHS_BLOCK = '\u258d';
    private const char LEFT_HALF_BLOCK = '\u258c';
    private const char LEFT_FIVE_EIGHTHS_BLOCK = '\u258b';
    private const char LEFT_THREE_QUARTERS_BLOCK = '\u258a';
    private const char LEFT_SEVEN_EIGHTHS_BLOCK = '\u2589';
    private const char FULL_BLOCK = '\u2588';
    // ReSharper restore InconsistentNaming

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
        target[0] = RIGHT_ONE_EIGHTH_BLOCK;
        target[26] = LEFT_ONE_EIGHTH_BLOCK;
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
            target[index + 1] = FULL_BLOCK;
        }

        if (fraction != 0)
        {
            target[whole + 1] = fraction switch
            {
                1 => LEFT_ONE_EIGHTH_BLOCK,
                2 => LEFT_ONE_QUARTER_BLOCK,
                3 => LEFT_THREE_EIGHTHS_BLOCK,
                4 => LEFT_HALF_BLOCK,
                5 => LEFT_FIVE_EIGHTHS_BLOCK,
                6 => LEFT_THREE_QUARTERS_BLOCK,
                7 => LEFT_SEVEN_EIGHTHS_BLOCK,
                _ => throw new ArgumentOutOfRangeException(nameof(fraction), fraction, null),
            };
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