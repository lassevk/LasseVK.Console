using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LasseVK.Console.Ansi;

/// <summary>
/// Base class for all Ansi writers. Descendants will wrap a target writer or other
/// type of object, that Ansi codes and text can be written to.
/// </summary>
/// <typeparam name="T">
/// The type of the descendant writer.
/// </typeparam>
public abstract partial class AnsiWriter<T>
    where T : AnsiWriter<T>
{
    // ReSharper disable UnusedParameter.Local
    private T Write([InterpolatedStringHandlerArgument("")] ref AppendHandler handler) =>
        // Do nothing
        (T)this;
    // ReSharper restore UnusedParameter.Local

    /// <summary>
    /// Write the given text to the underlying writer.
    /// </summary>
    /// <param name="text">
    /// The text to write.
    /// </param>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    protected abstract T Write(string text);

    /// <summary>
    /// Write the given value to the underlying writer.
    /// </summary>
    /// <param name="value">
    /// The value to write.
    /// </param>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    protected abstract T Write(int value);

    /// <summary>
    /// Write the given value to the underlying writer.
    /// </summary>
    /// <param name="value">
    /// The value to write.
    /// </param>
    /// <typeparam name="TValue">
    /// The type of the value to write.
    /// </typeparam>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    protected abstract T Write<TValue>(TValue value);

    /// <summary>
    /// AppendHandler is used to implement interpolated strings.
    /// </summary>
    [InterpolatedStringHandler]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly ref struct AppendHandler
    {
        private readonly AnsiWriter<T> _writer;

        /// <summary>
        /// Not for public consumption, used by generated code.
        /// </summary>
        public AppendHandler(int literalLength, int formattedCount, AnsiWriter<T> writer)
        {
            _writer = writer;
        }

        /// <summary>
        /// Not for public consumption, used by generated code.
        /// </summary>
        public void AppendLiteral(string s)
        {
            _writer.Write(s);
        }

        /// <summary>
        /// Not for public consumption, used by generated code.
        /// </summary>
        public void AppendFormatted<TValue>(TValue value)
        {
            _writer.Write(value);
        }

        /// <summary>
        /// Not for public consumption, used by generated code.
        /// </summary>
        public void AppendFormatted(int i)
        {
            _writer.Write(i);
        }
    }
}