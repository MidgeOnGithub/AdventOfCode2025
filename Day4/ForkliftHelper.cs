namespace Day4;

public class ForkliftHelper
{
    public ulong AccessibleRollCount { get; private set; }

    public IReadOnlyList<(int row, int column)> FindAccessibleRolls(IReadOnlyList<IReadOnlyList<int>> rollPlacements)
    {
        const int threshold = 4;
        int columns = rollPlacements[0].Count;

        var accessibleRolls = new List<(int column, int row)>();

        // Search each row.
        for (var rowNumber = 0; rowNumber < rollPlacements.Count; rowNumber++)
        {
            var row = rollPlacements[rowNumber];

            // Search each column.
            for (var columnNumber = 0; columnNumber < columns; columnNumber++)
            {
                if (row[columnNumber] == 0)
                    continue; // 0 is not a roll.

                bool columnBehind = columnNumber != 0;
                bool columnAhead = columnNumber != columns - 1;

                var adjacentRolls = 0;

                // Check current row adjacency.
                if (columnBehind)
                    adjacentRolls += row[columnNumber - 1];
                if (columnAhead)
                    adjacentRolls += row[columnNumber + 1];

                // Check previous row adjacency.
                if (rowNumber != 0)
                {
                    var previousRow = rollPlacements[rowNumber - 1];
                    adjacentRolls += previousRow[columnNumber]; // Current column.
                    if (columnBehind)
                        adjacentRolls += previousRow[columnNumber - 1];
                    if (columnAhead)
                        adjacentRolls += previousRow[columnNumber + 1];
                }

                // Check next row adjacency.
                if (rowNumber != rollPlacements.Count - 1)
                {
                    var nextRow = rollPlacements[rowNumber + 1];
                    adjacentRolls += nextRow[columnNumber]; // Current column.
                    if (columnBehind)
                        adjacentRolls += nextRow[columnNumber - 1];
                    if (columnAhead)
                        adjacentRolls += nextRow[columnNumber + 1];
                }

                if (adjacentRolls >= threshold)
                    continue;

                accessibleRolls.Add((rowNumber, columnNumber));
                AccessibleRollCount++;
            }
        }

        return accessibleRolls;
    }
}