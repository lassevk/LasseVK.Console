
using System.Text;

using LasseVK.Console;
using LasseVK.Console.Ansi;

System.Console.OutputEncoding = Encoding.UTF8;

var line = new ConsoleLine();

const char upperLeftQuadrant = '\u259b';
const char upperRightQuadrant = '\u259c';
const char lowerLeftQuadrant = '\u2599';
const char lowerRightQuadrant = '\u259f';
const char upperLeft = '\u2598';
const char upperRight = '\u259d';
const char lowerLeft = '\u2596';
const char lowerRight = '\u2597';
const char lowerHalf = '\u2584';
const char upperHalf = '\u2580';
const char leftHalf = '\u258c';
const char rightHalf = '\u2590';

string[] frames = [
    "" + upperLeftQuadrant + " ",
    "" + leftHalf + " ",
    "" + lowerLeftQuadrant + " ",
    "" + lowerHalf + " ",
    "" + lowerRight + lowerLeft,
    " " + lowerHalf,
    " " + lowerRightQuadrant,
    " " + rightHalf,
    " " + upperRightQuadrant,
    " " + upperHalf,
    "" + upperRight + upperLeft,
    "" + upperHalf + " ",
];
int index = 0;

while (true)
{
    line.Text = frames[index] + " " + index;
    index = (index + 1) % frames.Length;

    await Task.Delay(50);
}