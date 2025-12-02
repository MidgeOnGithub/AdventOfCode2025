namespace Day1;

public static class InputParser
{
    public static (Direction, uint distance) Parse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Input must be provided");

        char firstChar = input[0];
        Direction direction = firstChar switch
        {
            'L' => Direction.Left,
            'R' => Direction.Right,
            _   => throw new ArgumentException("Valid direction not provided"),
        };

        return uint.TryParse(input.AsSpan(1), out uint distance)
            ? (direction, distance)
            : throw new ArgumentException("Valid distance not provided");
    }
}