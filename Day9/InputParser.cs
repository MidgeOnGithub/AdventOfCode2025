namespace Day9;

public static class InputParser
{
    public static IReadOnlyList<Point> Parse(string input)
    {
        string[] lines = input.Split(Environment.NewLine);

        // Determine points and grid size.
        var gridWidth = 1;
        var gridHeight = 1;
        var tiles = new List<(int column, int row)>();
        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            if (parts.Length != 2)
                throw new ArgumentException($"Invalid line {line}, expected column,row", nameof(input));

            int column = int.Parse(parts[0]);
            int row = int.Parse(parts[1]);

            if (column > gridWidth)
                gridWidth = column + 1;
            if (row > gridHeight)
                gridHeight = row + 1;
            tiles.Add((column, row));
        }

        return tiles;
    }
}