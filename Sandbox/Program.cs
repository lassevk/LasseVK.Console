
using LasseVK.Console.Ansi;

for (int i = 1; i <= 3; i++)
{
    Console.WriteLine(i);
}

Thread.Sleep(1000);
Console.Out.WithAnsi().MoveUp(2);

Console.WriteLine("Hello world");