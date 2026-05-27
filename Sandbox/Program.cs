
using System.Globalization;

using LasseVK.Console;

using var lines = new ConsoleLines(10);

for (int index = 0; index < 100; index++)
{
    var dt = DateTime.Now;
    if (dt.Second % 5 == 0)
        lines.Set(Random.Shared.Next(10), dt.ToString("HH:mm"));
    else
        lines.Set(Random.Shared.Next(10), dt.ToString("HH:mm:ss"));

    if (dt.Second % 7 == 0)
        lines.Insert(0, "new line");

    Thread.Sleep(100);
}