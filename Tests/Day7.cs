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
            new() { E, E, E, E, E, E, E, S, E, E, E, E, E, E, E }, // 0
            new() { E, E, E, E, E, E, E, E, E, E, E, E, E, E, E }, // 1
            new() { E, E, E, E, E, E, E, V, E, E, E, E, E, E, E }, // 2
            new() { E, E, E, E, E, E, E, E, E, E, E, E, E, E, E }, // 3
            new() { E, E, E, E, E, E, V, E, V, E, E, E, E, E, E }, // 4
            new() { E, E, E, E, E, E, E, E, E, E, E, E, E, E, E }, // 5
            new() { E, E, E, E, E, V, E, V, E, V, E, E, E, E, E }, // 6
            new() { E, E, E, E, E, E, E, E, E, E, E, E, E, E, E }, // 7
            new() { E, E, E, E, V, E, V, E, E, E, V, E, E, E, E }, // 8
            new() { E, E, E, E, E, E, E, E, E, E, E, E, E, E, E }, // 9
            new() { E, E, E, V, E, V, E, E, E, V, E, V, E, E, E }, // 10
            new() { E, E, E, E, E, E, E, E, E, E, E, E, E, E, E }, // 11
            new() { E, E, V, E, E, E, V, E, E, E, E, E, V, E, E }, // 12
            new() { E, E, E, E, E, E, E, E, E, E, E, E, E, E, E }, // 13
            new() { E, V, E, V, E, V, E, V, E, V, E, E, E, V, E }, // 14
            new() { E, E, E, E, E, E, E, E, E, E, E, E, E, E, E }, // 15
        };
        const ulong expectedSplits = 21;

        // Act
        ulong totalSplits = TachyonAnalyzer.CountSplits(input);

        // Assert
        await Assert.That(totalSplits).IsEqualTo(expectedSplits);
    }

    [Test]
    public async Task Part2ExampleProducesExpectedOutput()
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
        const ulong expectedTimelines = 40;

        // Act
        ulong totalTimelines = TachyonAnalyzer.CountTimelines(input);

        // Assert
        await Assert.That(totalTimelines).IsEqualTo(expectedTimelines);
    }
}