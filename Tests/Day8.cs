using Day8;

namespace Tests;

public class Day8
{
    [Test]
    public async Task InputParserProducesExpectedOutput()
    {
        // Arrange
        const string input = "162,817,812\r\n" +
                             "57,618,57\r\n"   +
                             "906,360,560\r\n" +
                             "592,479,940\r\n" +
                             "352,342,300\r\n" +
                             "466,668,158\r\n" +
                             "542,29,236\r\n"  +
                             "431,825,988\r\n" +
                             "739,650,466\r\n" +
                             "52,470,668\r\n"  +
                             "216,146,977\r\n" +
                             "819,987,18\r\n"  +
                             "117,168,530\r\n" +
                             "805,96,715\r\n"  +
                             "346,949,466\r\n" +
                             "970,615,88\r\n"  +
                             "941,993,340\r\n" +
                             "862,61,35\r\n"   +
                             "984,92,344\r\n"  +
                             "425,690,689";
        var expectedJunctionBoxes = new List<JunctionBox>()
        {
            new(0, 162, 817, 812),
            new(1, 57, 618, 57),
            new(2, 906, 360, 560),
            new(3, 592, 479, 940),
            new(4, 352, 342, 300),
            new(5, 466, 668, 158),
            new(6, 542, 29, 236),
            new(7, 431, 825, 988),
            new(8, 739, 650, 466),
            new(9, 52, 470, 668),
            new(10,216, 146, 977),
            new(11,819, 987, 18),
            new(12,117, 168, 530),
            new(13,805, 96, 715),
            new(14,346, 949, 466),
            new(15,970, 615, 88),
            new(16,941, 993, 340),
            new(17,862, 61, 35),
            new(18,984, 92, 344),
            new(19,425, 690, 689),
        };

        // Act
        var result = InputParser.Parse(input);

        // Assert
        await Assert.That(result).IsEquivalentTo(expectedJunctionBoxes);
    }

    [Test]
    public async Task Part1ExampleProducesExpectedOutput()
    {
        // Arrange
        const int circuitsToConnect = 10;
        const int numberOfLargestCircuits = 3;
        const ulong expectedSizeOfLargestCircuitsCombined = 40;
        var input = new List<JunctionBox>()
        {
            new(0, 162, 817, 812),
            new(1, 57, 618, 57),
            new(2, 906, 360, 560),
            new(3, 592, 479, 940),
            new(4, 352, 342, 300),
            new(5, 466, 668, 158),
            new(6, 542, 29, 236),
            new(7, 431, 825, 988),
            new(8, 739, 650, 466),
            new(9, 52, 470, 668),
            new(10, 216, 146, 977),
            new(11, 819, 987, 18),
            new(12, 117, 168, 530),
            new(13, 805, 96, 715),
            new(14, 346, 949, 466),
            new(15, 970, 615, 88),
            new(16, 941, 993, 340),
            new(17, 862, 61, 35),
            new(18, 984, 92, 344),
            new(19, 425, 690, 689),
        };
        var expectedLargestCircuitSizes = new List<int> { 5, 4, 2 };

        var finder = new CircuitFinder();

        // Act
        finder.CreateShortestCircuits(input, circuitsToConnect);
        var largestCircuits = finder.NLargestCircuits(numberOfLargestCircuits);
        ulong largestCircuitsSize = finder.NLargestCircuitsSize(numberOfLargestCircuits);

        // Assert
        await Assert.That(largestCircuits.Select(c => c.Size)).IsEquivalentTo(expectedLargestCircuitSizes);
        await Assert.That(largestCircuitsSize).IsEquivalentTo(expectedSizeOfLargestCircuitsCombined);
    }

    [Test]
    public async Task Part2ExampleProducesExpectedOutput()
    {
        // Arrange
        var input = new List<JunctionBox>()
        {
            new(0, 162, 817, 812),
            new(1, 57, 618, 57),
            new(2, 906, 360, 560),
            new(3, 592, 479, 940),
            new(4, 352, 342, 300),
            new(5, 466, 668, 158),
            new(6, 542, 29, 236),
            new(7, 431, 825, 988),
            new(8, 739, 650, 466),
            new(9, 52, 470, 668),
            new(10, 216, 146, 977),
            new(11, 819, 987, 18),
            new(12, 117, 168, 530),
            new(13, 805, 96, 715),
            new(14, 346, 949, 466),
            new(15, 970, 615, 88),
            new(16, 941, 993, 340),
            new(17, 862, 61, 35),
            new(18, 984, 92, 344),
            new(19, 425, 690, 689),
        };
        var expectedFinalPair = (
            new JunctionBox(10, 216, 146, 977),
            new JunctionBox(12, 117, 168, 530)
        );

        var finder = new CircuitFinder();

        // Act
        (JunctionBox, JunctionBox) pair = finder.FindFinalConnectingPair(input);

        // Assert
        await Assert.That(pair.Item1).IsEqualTo(expectedFinalPair.Item1);
        await Assert.That(pair.Item2).IsEqualTo(expectedFinalPair.Item2);
    }
}