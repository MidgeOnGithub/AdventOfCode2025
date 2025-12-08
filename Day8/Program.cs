using Day8;

string input = File.ReadAllText("input.txt");
var boxes = InputParser.Parse(input);
Console.WriteLine($"Number of boxes: {boxes.Count}");

var finder = new CircuitFinder(1000);
finder.CreateShortestCircuits(boxes);

var largestCircuits = finder.NLargestCircuits(3);
Console.WriteLine($"Largest circuit sizes: {string.Join(',', largestCircuits.Select(c => c.Size))}");

Console.WriteLine(finder.NLargestCircuitsSize(3));