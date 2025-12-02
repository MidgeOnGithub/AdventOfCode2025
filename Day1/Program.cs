using AdventOfCode2025;

string[] inputs = File.ReadAllLines("input.txt");

var instructions = new List<(Direction, uint)>(inputs.Length);
instructions.AddRange(inputs.Select(InputParser.Parse));

Dial dial = new();
foreach ((Direction direction, uint distance) in instructions)
{
    dial.Rotate(direction, distance);
    Console.WriteLine($"Rotated {direction.ToString()} {distance}, ended at {dial.Value} with {dial.ZeroesSeen} zeroes seen.");
}

Console.WriteLine($"Zeroes seen: {dial.ZeroesSeen}");