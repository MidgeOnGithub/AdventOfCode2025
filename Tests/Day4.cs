using Day4;
using System.Threading.Tasks;

namespace Tests;

public class Day4
{
    [Test]
    public async Task Part1ExampleProducesExpectedOutput()
    {
        // Arrange
        var arrangement = new List<List<int>>
        {
            new () { 0, 0, 1, 1, 0, 1, 1, 1, 1, 0 },
            new () { 1, 1, 1, 0, 1, 0, 1, 0, 1, 1 },
            new () { 1, 1, 1, 1, 1, 0, 1, 0, 1, 1 },
            new () { 1, 0, 1, 1, 1, 1, 0, 0, 1, 0 },
            new () { 1, 1, 0, 1, 1, 1, 1, 0, 1, 1 },
            new () { 0, 1, 1, 1, 1, 1, 1, 1, 0, 1 },
            new () { 0, 1, 0, 1, 0, 1, 0, 1, 1, 1 },
            new () { 1, 0, 1, 1, 1, 0, 1, 1, 1, 1 },
            new () { 0, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
            new () { 1, 0, 1, 0, 1, 1, 1, 0, 1, 0 },
        };
        const ulong expectedAccessibleRollCount = 13;
        var expectedAccessibleRolls = new List<(int row, int column)>
        {
            (0, 2), (0, 3), (0, 5), (0, 6), (0, 8),
            (1, 0),
            (2, 6),
            (4, 0), (4, 9),
            (7, 0),
            (9, 0), (9, 2), (9, 8),
        };

        // Act
        var helper = new ForkliftHelper();
        var accessibleRolls = helper.FindAccessibleRolls(arrangement);

        // Assert
        await Assert.That(accessibleRolls).IsEquivalentTo(expectedAccessibleRolls);
        await Assert.That(helper.AccessibleRollCount).IsEqualTo(expectedAccessibleRollCount);
    }
}