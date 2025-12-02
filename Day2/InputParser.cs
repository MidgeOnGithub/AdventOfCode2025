namespace Day2;

public static class InputParser
{
    public static IReadOnlyList<(uint, uint)> Parse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("ID range must be provided");

        string[] inputRange = input.Split(',');
        var ranges = new List<(uint, uint)>(inputRange.Length);
        foreach (string range in inputRange)
        {
            string[] ids = range.Split('-');
            if (ids.Length != 2)
                throw new ArgumentException($"Invalid ID range: {range}");

            // Input is guaranteed to have the lower number first.
            string lower = ids[0];
            string upper = ids[1];

            if (!uint.TryParse(lower, out uint lowerNumber))
                throw new ArgumentException("Invalid lower ID number: {lower}");
            if (!uint.TryParse(upper, out uint upperNumber))
                throw new ArgumentException("Invalid lower ID number: {upper}");

            ranges.Add((lowerNumber, upperNumber));
        }

        return ranges;
    }
}