
using LasseVK.Console;

var line = new ConsoleLine();

while (true)
{
    var dt = DateTime.Now;
    if (dt.Second % 5 == 0)
        line.Set(dt.ToString("HH:mm"));
    else
        line.Set(dt.ToString("HH:mm:ss"));
}