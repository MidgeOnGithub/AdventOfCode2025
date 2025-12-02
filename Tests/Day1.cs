using AdventOfCode2025;

namespace Tests;

public class Day1
{
    [Test]
    public async Task ExampleProducesExpectedOutput()
    {
        // Arrange
        List<(Direction, uint)> instructions =
        [
            (Direction.Left, 68),
            (Direction.Left, 30),
            (Direction.Right, 48),
            (Direction.Left, 5),
            (Direction.Right, 60),
            (Direction.Left, 55),
            (Direction.Left, 1),
            (Direction.Left, 99),
            (Direction.Right, 14),
            (Direction.Left, 82),
        ];
        const uint expectedZeroesSeen = 3;
        Dial dial = new();

        // Act
        foreach ((Direction direction, uint distance) in instructions)
            dial.Rotate(direction, distance);

        // Assert
        await Assert.That(dial.ZeroesSeen).IsEqualTo(expectedZeroesSeen);
    }

    [Test]
    public async Task OverRotationsDoNotOverflow()
    {
        // Arrange
        const Direction firstDirection = Direction.Right;
        const uint firstDistance = 233;
        const uint expectedFirstValue = 83;

        const Direction secondDirection = Direction.Left;
        const uint secondDistance = 983;
        const uint expectedSecondValue = 0;

        const uint expectedZeroesSeen = 1;
        Dial dial = new();

        // Act
        dial.Rotate(firstDirection, firstDistance);
        uint firstValue = dial.Value;
        dial.Rotate(secondDirection, secondDistance);
        uint secondValue = dial.Value;

        // Assert
        await Assert.That(firstValue).IsEqualTo(expectedFirstValue);
        await Assert.That(secondValue).IsEqualTo(expectedSecondValue);
        await Assert.That(dial.ZeroesSeen).IsEqualTo(expectedZeroesSeen);
    }
}