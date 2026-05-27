using System.Globalization;
using System.Text;

namespace LasseVK.Console;

public static class ProgressBar
{
    public const int Length = 34;

    private static readonly char[] _blocks = [' ', '\u258f', '\u258e', '\u258d', '\u258c', '\u258b', '\u258a', '\u2589', '\u2588'];

    public static void FormatTo(Span<char> target, int progress, int total)
    {
        if (target.Length < Length)
        {
            throw new InvalidOperationException("Target buffer is too small");
        }

        decimal percent = progress * 100.0M / total;
        decimal blockCount = percent / 4M;

        int whole = (int)Math.Floor(blockCount);
        int fraction = (int)Math.Floor((blockCount - Math.Floor(blockCount)) * 8);

        // result = "[                         ] 100.0%"
        //                     1         2         3
        //           0123456789012345678901234567890123
        target[0] = '[';
        target[26] = ']';
        target[27] = ' ';
        target[33] = '%';
        int padding = 0;
        if (percent < 10.0M)
        {
            padding = 2;
        }
        else if (percent < 100.0M)
        {
            padding = 1;
        }

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

    public static string Format(int progress, int total)
    {
        Span<char> buffer = stackalloc char[Length];
        FormatTo(buffer, progress, total);
        return buffer.ToString();
    }

    public static void FormatTo(StringBuilder target, int progress, int total)
    {
        Span<char> buffer = stackalloc char[Length];
        FormatTo(buffer, progress, total);
        target.Append(buffer);
    }

    public static void FormatTo(TextWriter target, int progress, int total)
    {
        Span<char> buffer = stackalloc char[Length];
        FormatTo(buffer, progress, total);
        target.Write(buffer);
    }
}