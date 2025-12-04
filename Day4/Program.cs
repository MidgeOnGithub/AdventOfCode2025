using Day4;

string[] inputs = File.ReadAllLines("input.txt");

var arrangement = new List<IReadOnlyList<int>>(inputs.Length);
arrangement.AddRange(inputs.Select(InputParser.Parse));

var helper = new ForkliftHelper();
var accessibleRolls = helper.FindAccessibleRolls(arrangement);

Console.WriteLine($"Accessible rolls: {string.Join(", ", accessibleRolls)}");
Console.WriteLine($"Found {helper.AccessibleRollCount} accessible rolls.");