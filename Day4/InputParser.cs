namespace Day4;

public static class InputParser
{
    public static List<int> Parse(string input)
    {
        var rollPlacements = new List<int>(input.Length);
        rollPlacements.AddRange(input.Select(c => c == '@' ? 1 : 0));

        return rollPlacements;
    }
}