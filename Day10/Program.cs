using Day10;

string input = File.ReadAllText("input.txt");
List<Machine> machines = InputParser.Parse(input);
Console.WriteLine($"Number of machines: {machines.Count}");

List<int> buttonPresses = ButtonOptimizer.FewestButtonPressesForMachines(machines);
Console.WriteLine($"Fewest button presses: {string.Join(", ", buttonPresses)}");
Console.WriteLine($"Total button presses: {buttonPresses.Sum()}");