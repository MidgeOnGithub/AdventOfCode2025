namespace Day3;

public static class InputParser
{
    public static IReadOnlyList<uint> Parse(string bank)
    {
        if (string.IsNullOrWhiteSpace(bank))
            throw new ArgumentException("Input must be provided");

        var batteries = new List<uint>(bank.Length);
        batteries.AddRange(bank.Select(c => uint.Parse(c.ToString())));

        return batteries;
    }
}