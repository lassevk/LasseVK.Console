
using LasseVK.Console;

Console.OutputEncoding = System.Text.Encoding.UTF8;

for (int i = 0; i <= 100; i++)
{
    Console.WriteLine($"{i:000}: {ProgressBar.Format(i, 100)}");
}