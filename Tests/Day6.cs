using Day6;

namespace Tests;

public class Day6
{
    [Test]
    public async Task InputParserProducesExpectedOutput()
    {
        // Arrange
        const string input = "123 328  51 64\n" +
                             " 45 64  387 23 \n" +
                             "  6 98  215 314\n"   +
                             "*   +   *   +  ";
        var expectedProblems = new List<Problem>
        {
            new([123,  45,   6], Operation.Multiply),
            new([328,  64,  98], Operation.Add),
            new([ 51, 387, 215], Operation.Multiply),
            new([ 64,  23, 314], Operation.Add),
        };

        // Act
        var problems = InputParser.ParsePart1(input);
        Console.WriteLine(string.Join('\n', problems));

        // Assert
        await Assert.That(problems.Count).IsEqualTo(expectedProblems.Count);
        await Assert.That(problems).IsEquivalentTo(expectedProblems);
    }

    [Test]
    public async Task Part1ExampleProducesExpectedOutput()
    {
        // Arrange
        var problems = new List<Problem>
        {
            new([123,  45,   6], Operation.Multiply),
            new([328,  64,  98], Operation.Add),
            new([ 51, 387, 215], Operation.Multiply),
            new([ 64,  23, 314], Operation.Add),
        };
        const ulong expectedResult = 4277556;

        // Act
        ulong result = ProblemSolver.Solve(problems);

        // Assert
        await Assert.That(result).IsEqualTo(expectedResult);
    }
}