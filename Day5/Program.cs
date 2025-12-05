using Day5;

string input = File.ReadAllText("input.txt");

(var freshRanges, var available) = InputParser.Parse(input);
Console.WriteLine($"Fresh Ingredient Ranges: {freshRanges.Count}");
Console.WriteLine($"Available Ingredient Count: {available.Count}");

var finder = new ProduceFinder();
ulong totalAvailableFreshIngredients = finder.CountPossibleIngredients(freshRanges);
finder.FindUnspoiledFood(freshRanges, available);

Console.WriteLine($"Total possible fresh ingredients: {totalAvailableFreshIngredients}");
Console.WriteLine($"Unspoiled ingredient count: {finder.UnspoiledIngredientsCount}");