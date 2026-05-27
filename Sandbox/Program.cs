
using System.Globalization;

using LasseVK.Console;
using LasseVK.Console.Ansi;

Console.Out.WithAnsi().ClearEntireScreen();

using var lines = new ConsoleLines(10);

traverse(@"/Users/lassevk/Dev");

void traverse(string folderPath)
{
    lines.ScrollDownAndAppend(folderPath);

    foreach (string subFolderPath in Directory.GetDirectories(folderPath))
    {
        traverse(subFolderPath);
    }

    // foreach (string filePath in Directory.GetFiles(folderPath))
    // {
    //     lines.ScrollDownAndAppend(filePath);
    // }
}