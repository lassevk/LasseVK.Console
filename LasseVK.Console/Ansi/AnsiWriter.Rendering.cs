namespace LasseVK.Console.Ansi;

public partial class AnsiWriter<T>
{
    /// <summary>
    /// Resets all rendering attributes.
    /// </summary>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    public T ResetRendering() => Write("\e[0m");

    /// <summary>
    /// Set bold rendering.
    /// </summary>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    public T Bold() => Write("\e[1m");

    /// <summary>
    /// Set faint rendering.
    /// </summary>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    public T Faint() => Write("\e[2m");

    /// <summary>
    /// Set italic rendering.
    /// </summary>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    public T Italic() => Write("\e[3m");

    /// <summary>
    /// Set underline rendering.
    /// </summary>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    public T Underline() => Write("\e[4m");

    /// <summary>
    /// Set slow blink rendering.
    /// </summary>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    public T SlowBlink() => Write("\e[5m");

    /// <summary>
    /// Set rapid blink rendering, not widely supported.
    /// </summary>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    public T RapidBlink() => Write("\e[6m");

    /// <summary>
    /// Set reverse color rendering.
    /// </summary>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    public T Reverse() => Write("\e[7m");

    /// <summary>
    /// Set rendering to conceal to hide text. Can be marked and copied and pasted though.
    /// </summary>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    public T Conceal() => Write("\e[8m");

    /// <summary>
    /// Set strike-out rendering.
    /// </summary>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    public T CrossedOut() => Write("\e[9m");

    /// <summary>
    /// Set strike-out rendering, same as <see cref="CrossedOut"/>.
    /// </summary>
    /// <returns>
    /// The writer, for chaining.
    /// </returns>
    public T Strike() => CrossedOut();
}
