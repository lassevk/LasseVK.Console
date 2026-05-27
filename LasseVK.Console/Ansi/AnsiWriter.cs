using System.Runtime.CompilerServices;
using System.Text;

namespace LasseVK.Console.Ansi;

public abstract partial class AnsiWriter<T>
    where T : AnsiWriter<T>
{
    private T Write([InterpolatedStringHandlerArgument("")] ref AppendHandler handler) =>

        // Do nothing
        (T)this;

    protected abstract T Write(string text);
    protected abstract T Write(int value);
    protected abstract T Write<TValue>(TValue value);

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
}