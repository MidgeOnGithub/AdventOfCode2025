using Day11;

namespace Tests;

public class Day11
{
    [Test]
    public async Task InputParserProducesExpectedOutput()
    {
        // Arrange
        const string input =
            "aaa: you hhh\r\n"     +
            "you: bbb ccc\r\n"     +
            "bbb: ddd eee\r\n"     +
            "ccc: ddd eee fff\r\n" +
            "ddd: ggg\r\n"         +
            "eee: out\r\n"         +
            "fff: out\r\n"         +
            "ggg: out\r\n"         +
            "hhh: ccc fff iii\r\n" +
            "iii: out";
        var expectedDevices = new List<Device>
        {
            new("aaa", ["you", "hhh"]),
            new("you", ["bbb", "ccc"]),
            new("bbb", ["ddd", "eee"]),
            new("ccc", ["ddd", "eee", "fff"]),
            new("ddd", ["ggg"]),
            new("eee", ["out"]),
            new("fff", ["out"]),
            new("ggg", ["out"]),
            new("hhh", ["ccc", "fff", "iii"]),
            new("iii", ["out"]),
        };

        // Act
        IReadOnlyList<Device> devices = InputParser.Parse(input);

        // Assert
        await Assert.That(devices).IsEquivalentTo(expectedDevices);
    }

    [Test]
    public async Task Part1ExampleProducesExpectedOutputForPart1()
    {
        // Arrange
        var devices = new List<Device>
        {
            new("aaa", ["you", "hhh"]),
            new("you", ["bbb", "ccc"]),
            new("bbb", ["ddd", "eee"]),
            new("ccc", ["ddd", "eee", "fff"]),
            new("ddd", ["ggg"]),
            new("eee", ["out"]),
            new("fff", ["out"]),
            new("ggg", ["out"]),
            new("hhh", ["ccc", "fff", "iii"]),
            new("iii", ["out"]),
        };
        const int expectedPaths = 5;

        // Act
        int paths = PathFinder.FindPaths(devices);

        // Assert
        await Assert.That(paths).IsEqualTo(expectedPaths);
    }
}