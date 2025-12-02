using Day2;

string inputs = File.ReadAllText("input.txt");

var ranges = InputParser.Parse(inputs);

var validator = new IdValidator();
foreach ((uint, uint) range in ranges)
{
    var invalidIds = validator.FindInvalidIds(range.Item1, range.Item2);
    Console.WriteLine($"Found {invalidIds.Count} invalid IDs in range {range.Item1}-{range.Item2}:\n{string.Join(", ", invalidIds)}\nThe sum is {validator.Sum}.\n");
}

Console.WriteLine($"Sum of invalid IDs: {validator.Sum}");