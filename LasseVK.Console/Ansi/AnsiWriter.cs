using System.Runtime.CompilerServices;
using System.Text;

namespace LasseVK.Console.Ansi;

public abstract class AnsiWriter<T>
    where T : AnsiWriter<T>
{
    private void Write([InterpolatedStringHandlerArgument("")] ref AppendHandler handler)
    {
        // Do nothing
    }

    protected abstract void Write(string text);
    protected abstract void Write(int value);
    protected abstract void Write<TValue>(TValue value);

    [InterpolatedStringHandler]
    public readonly ref struct AppendHandler
    {
        // private readonly int _literalLength;
        // private readonly int _formattedCount;
        private readonly AnsiWriter<T> _writer;

        public AppendHandler(int literalLength, int formattedCount, AnsiWriter<T> writer)
        {
            // _literalLength = literalLength;
            // _formattedCount = formattedCount;
            _writer = writer;
        }

        public void AppendLiteral(string s)
        {
            _writer.Write(s);
        }

        public void AppendFormatted<TValue>(TValue value)
        {
            _writer.Write(value);
        }

        public void AppendFormatted(int i)
        {
            _writer.Write(i);
        }
    }

    public T MoveUp(int amount = 1)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(amount, 0);

        if (amount == 0)
        {
            return (T)this;
        }

        if (amount == 1)
        {
            Write("\e[A");
            return (T)this;
        }

        Write($"\e[{amount}A");
        return (T)this;
    }
}