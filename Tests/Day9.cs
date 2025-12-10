using Day9;
using Point = (int column, int row);

namespace Tests;

public class Day9
{
    private const TileColor O = TileColor.Other;
    private const TileColor R = TileColor.Red;

    [Test]
    public async Task InputParserProducesExpectedOutput()
    {
        // Arrange
        const string input =
            "7,1\r\n"  +
            "11,1\r\n" +
            "11,7\r\n" +
            "9,7\r\n"  +
            "9,5\r\n"  +
            "2,5\r\n"  +
            "2,3\r\n"  +
            "7,3";
        /* # = Red, X = Green
            0  1  2  3  4  5  6  7  8  9  10 11 12
         0  .  .  .  .  .  .  .  .  .  .  .  .  .
         1  .  .  .  .  .  .  .  #  X  X  X  #  .
         2  .  .  .  .  .  .  .  X  X  X  X  X  .
         3  .  .  #  X  X  X  X  #  X  X  X  X  .
         4  .  .  X  X  X  X  X  X  X  X  X  X  .
         5  .  .  #  X  X  X  X  X  X  #  X  X  .
         6  .  .  .  .  .  .  .  .  .  X  X  X  .
         7  .  .  .  .  .  .  .  .  .  #  X  #  .
        */
        var expectedRedTiles = new List<Point>
        {
            (7, 1),
            (11, 1),
            (11, 7),
            (9, 7),
            (9, 5),
            (2, 5),
            (2, 3),
            (7, 3),
        };

        // Act
        IReadOnlyList<Point> redTiles = InputParser.Parse(input);

        // Assert
        await Assert.That(redTiles).IsEquivalentTo(expectedRedTiles);
    }

    [Test]
    public async Task Part1ExampleProducesExpectedOutputForPart1()
    {
        // Arrange
        var redTiles = new List<Point>
        {
            (7, 1),
            (11, 1),
            (11, 7),
            (9, 7),
            (9, 5),
            (2, 5),
            (2, 3),
            (7, 3),
        };
        const ulong expectedLargestArea = 50;

        // Act
        (Point, Point) bounds = RectangleFinder.FindRectanglePart1(redTiles, out ulong largestArea);

        // Assert
        await Assert.That(largestArea).IsEqualTo(expectedLargestArea);
    }

    [Test]
    public async Task Part2ExampleProducesExpectedOutputForPart2()
    {
        // Arrange
        /* # = Red, X = Green
         .  .  .  .  .  .  .  .  .  .  .  .
         .  .  .  .  .  .  #  X  X  X  #  .
         .  .  .  .  .  .  X  X  X  X  X  .
         .  #  X  X  X  X  #  X  X  X  X  .
         .  X  X  X  X  X  X  X  X  X  X  .
         .  #  X  X  X  X  X  X  #  X  X  .
         .  .  .  .  .  .  .  .  X  X  X  .
         .  .  .  .  .  .  .  .  #  X  #  .
         */
        var redTiles = new List<Point>
        {
            (7, 1),
            (11, 1),
            (11, 7),
            (9, 7),
            (9, 5),
            (2, 5),
            (2, 3),
            (7, 3),
        };
        const ulong expectedLargestArea = 24;

        // Act
        (Point, Point) bounds = RectangleFinder.FindRectanglePart2(redTiles, out ulong largestArea);

        // Assert
        await Assert.That(largestArea).IsEqualTo(expectedLargestArea);
    }
}