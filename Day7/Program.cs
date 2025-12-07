using Day7;

string input = File.ReadAllText("input.txt");
ulong splits = TachyonAnalyzer.CountSplits(InputParser.Parse(input));
ulong timelines = TachyonAnalyzer.CountTimelines(InputParser.Parse(input));

Console.WriteLine($"Total number of splits: {splits}");
Console.WriteLine($"Total number of timelines: {timelines}");