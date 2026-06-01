using System.Globalization;
using System.Text;

namespace LasseVK.Console;

/// <summary>
/// This class provides static methods for formatting progress circles, small circles that
/// show progress in 25% increments.
/// </summary>
public static class ProgressCircle
{
    /// <summary>
    /// This is the length of the text output for the progress circle.
    /// If you provide a character buffer, it must be at least this length.
    /// </summary>
    public const int Length = 8;

    static ProgressCircle()
    {
        System.Console.OutputEncoding = Encoding.UTF8;
    }

    private static readonly char[] _circles = ['\u25cb', '\u25d4', '\u25d1', '\u25d5', '\u25cf'];

    /// <summary>
    /// Formats the progress circle to the specified buffer.
    /// </summary>
    /// <param name="target">
    /// The buffer to write the progress circle to. This must be at least <see cref="Length"/> characters long.
    /// </param>
    /// <param name="progress">
    /// The current progress of the process or what the progress circle represents.
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
        decimal ppm = (int)(percent * 10m);

        // result = "O 100.0%"
        // result = "O  50.0%"
        // result = "O   5.0%"
        //           01234567
        target[0] = ppm switch
        {
            <= 125 => _circles[0]
          , <= 375 => _circles[1]
          , <= 625 => _circles[2]
          , <= 875 => _circles[3]
          , _      => _circles[4]
           ,
        };
        target[1] = ' ';
        target[2] = ' ';
        target[7] = '%';
        int padding = percent switch
        {
            < 10.0M  => 2,
            < 100.0M => 1,
            _        => 0,
        };

        percent.TryFormat(target[(2 + padding)..], out int _, "0.0", CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Formats the progress circle and returns it as a string.
    /// </summary>
    /// <param name="progress">
    /// The current progress of the process or what the progress circle represents.
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
    /// Formats the progress circle to the specified <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="target">
    /// The buffer to write the progress circle to. This must be at least <see cref="Length"/> characters long.
    /// </param>
    /// <param name="progress">
    /// The current progress of the process or what the progress circle represents.
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
    /// Formats the progress circle and writes it to the specified <see cref="TextWriter"/>.
    /// </summary>
    /// <param name="target">
    /// The buffer to write the progress circle to. This must be at least <see cref="Length"/> characters long.
    /// </param>
    /// <param name="progress">
    /// The current progress of the process or what the progress circle represents.
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