
using LasseVK.Console;

for (int i = 0; i <= 100; i++)
{
    Console.WriteLine($"{i:000}: {ProgressBar.Format(i, 100)}");
}