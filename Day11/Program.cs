using Day11;

string input = File.ReadAllText("input.txt");
List<Device> devices = InputParser.Parse(input);
Console.WriteLine($"Number of devices: {devices.Count}");

int pathsToEnd = PathFinder.FindPaths(devices);
Console.WriteLine($"Total paths: {pathsToEnd}");