using System.Collections.Concurrent;

namespace Day9;

public class RectangleFinder
{
    public static (Point, Point) FindRectanglePart1(IReadOnlyList<Point> redTiles, out ulong largestArea)
    {
        largestArea = 1;
        (Point,Point) largestRectangle = default;

        for (var i = 0; i < redTiles.Count; i++)
        {
            Point tile1 = redTiles[i];
            for (var j = 0; j < redTiles.Count; j++)
            {
                if (i == j)
                    continue;

                Point tile2 = redTiles[j];

                ulong width = (ulong) Math.Abs(tile2.column - tile1.column) + 1;
                ulong height = (ulong) Math.Abs(tile2.row - tile1.row) + 1;
                ulong area = width * height;
                if (area <= largestArea)
                    continue;

                largestArea = area;
                largestRectangle = (tile1, tile2);
                Console.WriteLine($"New largest area {largestArea} at {tile1} to {tile2}.");
            }
        }

        return largestRectangle;
    }

    public static (Point, Point) FindRectanglePart2(IReadOnlyList<Point> redTiles, out ulong largestArea)
    {
        largestArea = 1;
        (Point,Point) largestRectangle = default;

        HashSet<(Point, Point)> redTilePairs = [];
        foreach (Point t in redTiles)
        {
            for (var j = 1; j < redTiles.Count; j++)
                redTilePairs.Add((t, redTiles[j]));
        }

        ConcurrentBag<(ulong area, Point tile1, Point tile2)> areaPairsBag = [];
        Parallel.ForEach(redTilePairs,
            tilePair =>
            {
                (Point tile1, Point tile2) = tilePair;
                ulong width = (ulong) Math.Abs(tile2.column - tile1.column) + 1;
                ulong height = (ulong) Math.Abs(tile2.row   - tile1.row)    + 1;
                ulong area = width * height;
                areaPairsBag.Add((area, tile1, tile2));
            });

        List<(ulong area, Point tile1, Point tile2)> areaPairs = areaPairsBag.ToList();
        Console.WriteLine($"Found {areaPairs.Count} area pairs.");

        List<Point> invalidTiles = [];
        foreach ((ulong area, Point tile1, Point tile2) in areaPairs.OrderByDescending(p => p.area))
        {
            bool tilesAreValid = TilesWithinAreValid(tile1, tile2, ref redTiles, ref invalidTiles);
            if (!tilesAreValid)
            {
                Console.WriteLine($"Invalid area {area} at {tile1} to {tile2}.");
                continue;
            }

            // The first valid pair is the largest since we're going by descending area.
            largestArea = area;
            largestRectangle = (tile1, tile2);
            break;
        }

        return largestRectangle;
    }

    private static bool TilesWithinAreValid(
        Point redTile1,
        Point redTile2,
        ref readonly IReadOnlyList<Point> redTiles,
        ref List<Point> invalidTiles
    )
    {
        // Get the rectangle column bounds.
        bool tile1ColumnIsLess = redTile1.column < redTile2.column;
        int colStart = tile1ColumnIsLess ? redTile1.column : redTile2.column;
        int colEnd = tile1ColumnIsLess ? redTile2.column : redTile1.column;

        // Get the rectangle row bounds.
        bool tile1RowIsLess = redTile1.row < redTile2.row;
        int rowStart = tile1RowIsLess ? redTile1.row : redTile2.row;
        int rowEnd = tile1RowIsLess ? redTile2.row : redTile1.row;

        if (invalidTiles.Any(p => p.column >= colStart && p.column < colEnd && p.row >= rowStart && p.row < rowEnd))
        {
            Console.WriteLine($"Skipping rectangle with invalid tile between {redTile1} to {redTile2}.");
            return false;
        }

        // Check perimeter first for an inexpensive potential shortcut to finding an invalid rectangle.
        IReadOnlyList<Point> tiles = redTiles;
        ConcurrentBag<Point> newInvalidTiles = [];
        Parallel.For(
            colStart,
            colEnd,
            i =>
            {
                // Bottom side on the border.
                if (!PointIsInsideRedTilePolygon(tiles, i, rowStart))
                    newInvalidTiles.Add(new Point(i, rowStart));

                // Inside the bottom side border.
                if (!PointIsInsideRedTilePolygon(tiles, i, rowStart + 1))
                    newInvalidTiles.Add(new Point(i, rowStart + 1));

                // Top side
                if (!PointIsInsideRedTilePolygon(tiles, i, rowEnd))
                    newInvalidTiles.Add(new Point(i, rowEnd));

                // Inside the top side border.
                if (!PointIsInsideRedTilePolygon(tiles, i, rowEnd - 1))
                    newInvalidTiles.Add(new Point(i, rowEnd - 1));
            }
        );
        if (!newInvalidTiles.IsEmpty)
        {
            invalidTiles.AddRange(newInvalidTiles);
            return false;
        }

        Parallel.For(
            rowStart,
            rowEnd,
            i =>
            {
                // Left side on the border.
                if (!PointIsInsideRedTilePolygon(tiles, colStart, i))
                    newInvalidTiles.Add(new Point(colStart, i));

                // Inside the left side border.
                if (!PointIsInsideRedTilePolygon(tiles, colStart + 1, i))
                    newInvalidTiles.Add(new Point(colStart + 1, i));

                // Right side on the border.
                if (!PointIsInsideRedTilePolygon(tiles, colEnd, i))
                    newInvalidTiles.Add(new Point(colEnd, i));

                // Inside the right side border.
                if (!PointIsInsideRedTilePolygon(tiles, colEnd - 1, i))
                    newInvalidTiles.Add(new Point(colEnd - 1, i));
            }
        );
        if (!newInvalidTiles.IsEmpty)
        {
            invalidTiles.AddRange(newInvalidTiles);
            return false;
        }

        // We've done the shortcuts, now we have to check the inner tiles.
        for (int i = rowStart + 2; i < rowEnd - 2; i++)
        {
            int i1 = i;
            Parallel.For(
                colStart + 2,
                colEnd   - 2,
                j =>
                {
                    if (!PointIsInsideRedTilePolygon(tiles, j, i1))
                        newInvalidTiles.Add((j, i1));
                }
            );

            if (!newInvalidTiles.IsEmpty)
            {
                invalidTiles.AddRange(newInvalidTiles);
                return false;
            }

            Console.WriteLine($"Validated row {i} for rectangle between {redTile1} to {redTile2}.");
        }

        return true;
    }

    private static bool PointIsInsideRedTilePolygon(IReadOnlyList<Point> redTiles, int checkCol, int checkRow)
    {
        var insidePolygon = false;
        for (var i = 0; i < redTiles.Count; i++)
        {
            Point point1 = redTiles[i];
            if (point1.column < checkCol)
                continue; // Test direction is to the right.

            // Use the last point if i==0.
            Point point2 = i == 0 ? redTiles[^1] : redTiles[i - 1];

            bool isRedTile =
                (point1.column == checkCol && point1.row == checkRow) ||
                (point2.column == checkCol && point2.row == checkRow);
            if (isRedTile)
                return true; // Red tile.

            // Horizontal line (points' rows match).
            if (point1.row == point2.row)
            {
                if (checkRow == point1.row)
                {
                    bool point1IsLeftOfCurrentTile = point1.column < checkCol;
                    bool point2IsLeftOfCurrentTile = point2.column < checkCol;
                    if (point1IsLeftOfCurrentTile != point2IsLeftOfCurrentTile)
                        return true; // On border.
                }

                continue; // Line does not intersect.
            }

            // Vertical line (points' columns match).
            bool point1IsAboveCurrentTile = point1.row >= checkRow;
            bool point2IsAboveCurrentTile = point2.row >= checkRow;
            if (point1IsAboveCurrentTile == point2IsAboveCurrentTile)
                continue; // Line does not intersect.

            if (checkCol == point1.column)
                return true; // On border.

            insidePolygon = !insidePolygon;
        }

        return insidePolygon;
    }
}