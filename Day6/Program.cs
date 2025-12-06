using Day6;

string input = File.ReadAllText("input.txt");

var problems = InputParser.ParsePart1(input);
ulong result = ProblemSolver.Solve(problems);

Console.WriteLine($"Total sum of problems: {result}");