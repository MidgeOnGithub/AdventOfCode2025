using Day7;

namespace Tests;

public class Day7
{
    private const Node E = Node.Empty;
    private const Node S = Node.Start;
    private const Node V = Node.Splitter;

    [Test]
    public async Task InputParserProducesExpectedOutput()
    {
        // Arrange
        const string input = ".......S.......\r\n" +
                             "...............\r\n" +
                             ".......^.......\r\n" +
                             "...............\r\n" +
                             "......^.^......\r\n" +
                             "...............\r\n" +
                             ".....^.^.^.....\r\n" +
                             "...............\r\n" +
                             "....^.^...^....\r\n" +
                             "...............\r\n" +
                             "...^.^...^.^...\r\n" +
                             "...............\r\n" +
                             "..^...^.....^..\r\n" +
                             "...............\r\n" +
                             ".^.^.^.^.^...^.\r\n" +
                             "...............";
        var expected = new List<List<Node>>()
        {
            new() { E, E, E, E, E, E, E, S, E, E, E, E, E, E, E },
            new() { E, E, E, E, E, E, E, E, E, E, E, E, E, E, E },
            new() { E, E, E, E, E, E, E, V, E, E, E, E, E, E, E },
            new() { E, E, E, E, E, E, E, E, E, E, E, E, E, E, E },
            new() { E, E, E, E, E, E, V, E, V, E, E, E, E, E, E },
            new() { E, E, E, E, E, E, E, E, E, E, E, E, E, E, E },
            new() { E, E, E, E, E, V, E, V, E, V, E, E, E, E, E },
            new() { E, E, E, E, E, E, E, E, E, E, E, E, E, E, E },
            new() { E, E, E, E, V, E, V, E, E, E, V, E, E, E, E },
            new() { E, E, E, E, E, E, E, E, E, E, E, E, E, E, E },
            new() { E, E, E, V, E, V, E, E, E, V, E, V, E, E, E },
            new() { E, E, E, E, E, E, E, E, E, E, E, E, E, E, E },
            new() { E, E, V, E, E, E, V, E, E, E, E, E, V, E, E },
            new() { E, E, E, E, E, E, E, E, E, E, E, E, E, E, E },
            new() { E, V, E, V, E, V, E, V, E, V, E, E, E, V, E },
            new() { E, E, E, E, E, E, E, E, E, E, E, E, E, E, E },
        };

        // Act
        var result = InputParser.Parse(input);

        // Assert
        await Assert.That(result).IsEquivalentTo(expected);
    }

    [Test]
    public async Task Part1ExampleProducesExpectedOutput()
    {
        // Arrange
        var input = new List<List<Node>>()
        {
            new() { E, E, E, E, E, E, E, S, E, E, E, E, E, E, E },
            new() { E, E, E, E, E, E, E, E, E, E, E, E, E, E, E },
            new() { E, E, E, E, E, E, E, V, E, E, E, E, E, E, E },
            new() { E, E, E, E, E, E, E, E, E, E, E, E, E, E, E },
            new() { E, E, E, E, E, E, V, E, V, E, E, E, E, E, E },
            new() { E, E, E, E, E, E, E, E, E, E, E, E, E, E, E },
            new() { E, E, E, E, E, V, E, V, E, V, E, E, E, E, E },
            new() { E, E, E, E, E, E, E, E, E, E, E, E, E, E, E },
            new() { E, E, E, E, V, E, V, E, E, E, V, E, E, E, E },
            new() { E, E, E, E, E, E, E, E, E, E, E, E, E, E, E },
            new() { E, E, E, V, E, V, E, E, E, V, E, V, E, E, E },
            new() { E, E, E, E, E, E, E, E, E, E, E, E, E, E, E },
            new() { E, E, V, E, E, E, V, E, E, E, E, E, V, E, E },
            new() { E, E, E, E, E, E, E, E, E, E, E, E, E, E, E },
            new() { E, V, E, V, E, V, E, V, E, V, E, E, E, V, E },
            new() { E, E, E, E, E, E, E, E, E, E, E, E, E, E, E },
        };
        const ulong expectedSplits = 21;

        // Act
        ulong totalSplits = TachyonAnalyzer.CountSplits(input);

        // Assert
        await Assert.That(totalSplits).IsEqualTo(expectedSplits);
    }
}