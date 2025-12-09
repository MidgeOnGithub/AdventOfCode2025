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
        var expectedPoints = new List<Point>()
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
        IReadOnlyList<Point> points = InputParser.Parse(input);

        // Assert
        await Assert.That(points).IsEquivalentTo(expectedPoints);
    }

    [Test]
    public async Task Part1ExampleProducesExpectedOutput()
    {
        // Arrange
        var redTiles = new List<Point>()
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
        (Point, Point) points = RectangleFinder.FindRectangle(redTiles, out ulong largestArea);

        // Assert
        await Assert.That(largestArea).IsEqualTo(expectedLargestArea);
    }
}