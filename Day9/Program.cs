global using Point = (int column, int row);

using Day9;

string input = File.ReadAllText("input.txt");
IReadOnlyList<Point> points = InputParser.Parse(input);

(Point, Point) rectangle = RectangleFinder.FindRectangle(points, out ulong largestArea);

Console.WriteLine($"Largest area is {largestArea} at {rectangle.Item1}-{rectangle.Item2}.");