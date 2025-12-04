using Day4;

string[] inputs = File.ReadAllLines("input.txt");

var arrangement = new List<List<int>>(inputs.Length);
arrangement.AddRange(inputs.Select(InputParser.Parse));

var helper = new ForkliftHelper();
var wave = 0;
while (helper.TryRemoveAccessibleRolls(arrangement, out var removedRolls))
{
    wave++;
    Console.WriteLine($"Wave {wave}: removing {removedRolls.Count} rolls: {string.Join(", ", removedRolls)}");
}

Console.WriteLine($"Removed {helper.AccessibleRollCount} rolls.");