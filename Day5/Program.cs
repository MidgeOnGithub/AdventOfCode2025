using Day5;

string input = File.ReadAllText("input.txt");

(var freshRanges, var available) = InputParser.Parse(input);
Console.WriteLine($"Fresh Ingredient Ranges: {freshRanges.Count}");
Console.WriteLine($"Available Ingredient Count: {available.Count}");

var finder = new ProduceFinder();
finder.FindUnspoiledFood(freshRanges, available);

Console.WriteLine($"Fresh Ingredient Count: {finder.Count}");