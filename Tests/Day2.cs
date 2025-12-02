using Day2;

namespace Tests;

public class Day2
{
    [Test]
    public async Task InputParserProducesExpectedOutput()
    {
        // Arrange
        const string input = "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,"     +
                             "1698522-1698528,446443-446449,38593856-38593862,565653-565659," +
                             "824824821-824824827,2121212118-2121212124";

        List<(uint, uint)> expected =
        [
            (11, 22),
            (95, 115),
            (998, 1012),
            (1188511880, 1188511890),
            (222220, 222224),
            (1698522, 1698528),
            (446443, 446449),
            (38593856, 38593862),
            (565653, 565659),
            (824824821, 824824827),
            (2121212118, 2121212124),
        ];

        // Act
        var result = InputParser.Parse(input);

        // Assert
        await Assert.That(result).IsEquivalentTo(expected);
    }

    [Test]
    [Skip("Invalid for Part 2")]
    public async Task Part1ExampleProducesExpectedOutput()
    {
        // Arrange
        var inputs = InputParser.Parse(
            "11-22,95-115,998-1012,1188511880-1188511890,222220-222224," +
            "1698522-1698528,446443-446449,38593856-38593862,565653-565659,"  +
            "824824821-824824827,2121212118-2121212124"
        );

        const ulong expectedOutput = 1227775554;
        var validator = new IdValidator();

        // Act
        foreach ((uint lower, uint upper) in inputs)
        {
            var ids = validator.FindInvalidIds(lower, upper);
            Console.WriteLine($"Found {ids.Count} invalid IDs in range {lower}-{upper}: {string.Join(", ", ids)}");
        }

        // Assert
        await Assert.That(validator.Sum).IsEqualTo(expectedOutput);
    }

    [Test]
    public async Task Part2ExampleProducesExpectedOutput()
    {
        // Arrange
        var inputs = InputParser.Parse(
            "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,"     +
            "1698522-1698528,446443-446449,38593856-38593862,565653-565659," +
            "824824821-824824827,2121212118-2121212124"
        );

        const ulong expectedOutput = 4174379265;
        var validator = new IdValidator();

        // Act
        foreach ((uint lower, uint upper) in inputs)
        {
            var ids = validator.FindInvalidIds(lower, upper);
            Console.WriteLine($"Found {ids.Count} invalid IDs in range {lower}-{upper}: {string.Join(", ", ids)}");
        }

        // Assert
        await Assert.That(validator.Sum).IsEqualTo(expectedOutput);
    }
}