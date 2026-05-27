namespace LasseVK.Console.Ansi;

public partial class AnsiWriter<T>
{
    public T ResetRendering() => Write("\e[0m");

    public T Bold() => Write("\e[1m");
    public T Faint() => Write("\e[2m");

    public T Italic() => Write("\e[3m");
    public T Underline() => Write("\e[4m");

    public T SlowBlink() => Write("\e[5m");
    public T RapidBlink() => Write("\e[6m");

    public T Reverse() => Write("\e[7m");
    public T Conceal() => Write("\e[8m");
    public T CrossedOut() => Write("\e[9m");
    public T Strike() => CrossedOut();
}
