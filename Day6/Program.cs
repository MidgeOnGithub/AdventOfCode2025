using Day6;

string input = File.ReadAllText("input.txt");

var part1Problems = InputParser.ParsePart1(input);
ulong part1Result = ProblemSolver.Solve(part1Problems);
Console.WriteLine($"Part 1 sum of problems: {part1Result}");

var part2Problems = InputParser.ParsePart2(input);
ulong part2Result = ProblemSolver.Solve(part2Problems);
Console.WriteLine($"Part 2 sum of problems: {part2Result}");