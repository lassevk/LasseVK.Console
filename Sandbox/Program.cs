
using LasseVK.Console.Ansi;

AnsiTextWriter ansiConsole = Console.Out.WithAnsi();
ansiConsole.HideCursor();

ansiConsole.Strike();
for (int i = 0; i <= 40; i++)
{
    Thread.Sleep(50);
    Console.Out.Write("A");
}
ansiConsole.ResetRendering();

// ansiConsole.ClearToBeginningOfLine();
ansiConsole.ShowCursor();
Console.WriteLine();