global using Point = (int column, int row);

using Day9;

string input = File.ReadAllText("input.txt");
IReadOnlyList<Point> redTiles = InputParser.Parse(input);

(Point, Point) rectangle = RectangleFinder.FindRectanglePart1(redTiles, out ulong largestArea);
Console.WriteLine($"Part 1: Largest area is {largestArea} at {rectangle.Item1} to {rectangle.Item2}.");

(Point, Point) rectangle2 = RectangleFinder.FindRectanglePart2(redTiles, out ulong largestArea2);
Console.WriteLine($"Part 2: Largest area is {largestArea2} at {rectangle2.Item1} to {rectangle2.Item2}.");