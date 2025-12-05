namespace Day5;

public static class InputParser
{
    public static (IReadOnlyList<(ulong lower, ulong upper)> freshRanges, IReadOnlyList<ulong> available) Parse(string input)
    {
        var freshRanges = new List<(ulong lower, ulong upper)>();
        var available = new List<ulong>();

        string[] lines = input.Split(Environment.NewLine);
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue; // This is the blank line between ranges and available ingredients.

            if (line.Contains("-"))
            {
                string[] parts = line.Split('-');
                ulong lower = ulong.Parse(parts[0]);
                ulong upper = ulong.Parse(parts[1]);
                freshRanges.Add((lower, upper));
                continue;
            }

            available.Add(ulong.Parse(line));
        }

        return (freshRanges, available);
    }
}