namespace Day9;

public static class InputParser
{
    public static IReadOnlyList<Point> Parse(string input)
    {
        string[] lines = input.Split(Environment.NewLine);

        // Determine red tiles and grid size.
        var gridWidth = 1;
        var gridHeight = 1;
        var redTiles = new List<Point>();
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
            redTiles.Add((column, row));
        }
        Console.WriteLine($"Added {redTiles.Count} red tiles with grid size {gridWidth}x{gridHeight}.");

        return redTiles;
    }
}