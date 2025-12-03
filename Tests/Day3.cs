using Day3;

namespace Tests;

public class Day3
{
    [Test]
    [Skip("Invalid for Part 2")]
    public async Task Part1ExampleProducesExpectedOutput()
    {
        // Arrange
        List<List<uint>> banks =
        [
            [9, 8, 7, 6, 5, 4, 3, 2, 1, 1, 1, 1, 1, 1, 1],
            [8, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 9],
            [2, 3, 4, 2, 3, 4, 2, 3, 4, 2, 3, 4, 2, 7, 8],
            [8, 1, 8, 1, 8, 1, 9, 1, 1, 1, 1, 2, 1, 1, 1],
        ];
        const uint expectedJolt = 357;
        var finder = new JoltFinder();

        // Act
        var batteriesSelected = finder.ProcessBanks(banks);

        // Assert
        await Assert.That(batteriesSelected[0]).IsEquivalentTo(new uint[] { 9, 8 });
        await Assert.That(batteriesSelected[1]).IsEquivalentTo(new uint[] { 8, 9 });
        await Assert.That(batteriesSelected[2]).IsEquivalentTo(new uint[] { 7, 8 });
        await Assert.That(batteriesSelected[3]).IsEquivalentTo(new uint[] { 9, 2 });
        await Assert.That(finder.Sum).IsEqualTo(expectedJolt);
    }

    [Test]
    public async Task Part2ExampleProducesExpectedOutput()
    {
        // Arrange
        List<List<uint>> banks =
        [
            [9, 8, 7, 6, 5, 4, 3, 2, 1, 1, 1, 1, 1, 1, 1],
            [8, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 9],
            [2, 3, 4, 2, 3, 4, 2, 3, 4, 2, 3, 4, 2, 7, 8],
            [8, 1, 8, 1, 8, 1, 9, 1, 1, 1, 1, 2, 1, 1, 1],
        ];
        var expectedFirstBankSelection  = new uint[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 1, 1, 1 };
        var expectedSecondBankSelection = new uint[] { 8, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 9 };
        var expectedThirdBankSelection  = new uint[] { 4, 3, 4, 2, 3, 4, 2, 3, 4, 2, 7, 8 };
        var expectedFourthBankSelection = new uint[] { 8, 8, 8, 9, 1, 1, 1, 1, 2, 1, 1, 1 };
        const ulong expectedJolt = 3121910778619;
        var finder = new JoltFinder();

        // Act
        var batteriesSelected = finder.ProcessBanks(banks);

        // Assert
        await Assert.That(batteriesSelected[0]).IsEquivalentTo(expectedFirstBankSelection);
        await Assert.That(batteriesSelected[1]).IsEquivalentTo(expectedSecondBankSelection);
        await Assert.That(batteriesSelected[2]).IsEquivalentTo(expectedThirdBankSelection);
        await Assert.That(batteriesSelected[3]).IsEquivalentTo(expectedFourthBankSelection);
        await Assert.That(finder.Sum).IsEqualTo(expectedJolt);
    }
}