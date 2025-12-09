namespace Day9;

public class RectangleFinder
{
    public static (Point, Point) FindRectangle(IReadOnlyList<Point> redTiles, out ulong largestArea)
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
                Console.WriteLine($"Found largest rectangle {largestArea} at {tile1}-{tile2}.");
            }
        }

        return largestRectangle;
    }
}