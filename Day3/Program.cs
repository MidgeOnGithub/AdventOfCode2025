using Day3;

string[] inputs = File.ReadAllLines("input.txt");

var banks = new List<IReadOnlyList<uint>>(inputs.Length);
banks.AddRange(inputs.Select(InputParser.Parse));

var finder = new JoltFinder();
finder.ProcessBanks(banks);

Console.WriteLine($"Sum of highest jolts from each bank is {finder.Sum}");