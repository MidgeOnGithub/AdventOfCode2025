using Day5;

namespace Tests;

public class Day5
{
    [Test]
    public async Task InputParserProducesExpectedOutput()
    {
        // Arrange
        string input = $"3-5{Environment.NewLine}"   +
                       $"10-14{Environment.NewLine}" +
                       $"16-20{Environment.NewLine}" +
                       $"12-18{Environment.NewLine}" +
                       $"{Environment.NewLine}"      +
                       $"1{Environment.NewLine}"     +
                       $"5{Environment.NewLine}"     +
                       $"8{Environment.NewLine}"     +
                       $"11{Environment.NewLine}"    +
                       $"17{Environment.NewLine}"    +
                       "32";
        var expectedFreshRanges = new List<(ulong lower, ulong upper)>{ (3, 5), (10, 14), (16, 20), (12, 18), };
        var expectedAvailable = new List<ulong>{ 1, 5, 8, 11, 17, 32, };

        // Act
        (var freshRanges, var available) = InputParser.Parse(input);

        // Assert
        await Assert.That(freshRanges).IsEquivalentTo(expectedFreshRanges);
        await Assert.That(available).IsEquivalentTo(expectedAvailable);
    }

    [Test]
    public async Task Part1ExampleProducesExpectedOutput()
    {
        // Arrange
        var freshRanges = new List<(ulong lower, ulong upper)>{ (3, 5), (10, 14), (16, 20), (12, 18), };
        var available = new List<ulong>{ 1, 5, 8, 11, 17, 32, };

        const uint expectedCount = 3;
        var expectedFreshIngredients = new List<ulong>{ 5, 11, 17 };
        var finder = new ProduceFinder();

        // Act
        var result = finder.FindUnspoiledFood(freshRanges, available);

        // Assert
        await Assert.That(result).IsEquivalentTo(expectedFreshIngredients);
        await Assert.That(finder.UnspoiledIngredientsCount).IsEqualTo(expectedCount);
    }

    [Test]
    public async Task Part2ExampleProducesExpectedOutput()
    {
        // Arrange
        var freshRanges = new List<(ulong lower, ulong upper)>{ (3, 5), (10, 14), (16, 20), (12, 18), };
        const uint expectedCount = 14;
        var finder = new ProduceFinder();

        // Act
        ulong possibleIngredients = finder.CountPossibleIngredients(freshRanges);

        // Assert
        await Assert.That(possibleIngredients).IsEqualTo(expectedCount);
    }
}