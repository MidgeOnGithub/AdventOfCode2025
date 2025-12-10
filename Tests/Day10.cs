using Day10;

namespace Tests;

public class Day10
{
    private static readonly List<Machine> ExampleMachines =
    [
        new()
        {
            Lights = [false, true, true, false],
            Buttons =
            [
                new Button(0, [3]),
                new Button(1, [1, 3]),
                new Button(2, [2]),
                new Button(3, [2, 3]),
                new Button(4, [0, 2]),
                new Button(5, [0, 1]),
            ],
            Joltages = [3, 5, 4, 7],
        },
        new()
        {
            Lights = [false, false, false, true, false],
            Buttons =
            [
                new Button(0, [0, 2, 3, 4]),
                new Button(1, [2, 3]),
                new Button(2, [0, 4]),
                new Button(3, [0, 1, 2]),
                new Button(4, [1, 2, 3, 4]),
            ],
            Joltages = [7, 5, 12, 7, 2],
        },
        new()
        {
            Lights = [false, true, true, true, false, true],
            Buttons = [
                new Button(0, [0, 1, 2, 3, 4]),
                new Button(1, [0, 3, 4]),
                new Button(2, [0, 1, 2, 4, 5]),
                new Button(3, [1, 2]),
            ],
            Joltages = [10, 11, 11, 5, 10, 5],
        },
    ];

    [Test]
    public async Task InputParserProducesExpectedOutput()
    {
        // Arrange
        const string input =
            "[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}\r\n"             +
            "[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}\r\n" +
            "[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}";
        List<Machine> expected = ExampleMachines;

        // Act
        List<Machine> machines = InputParser.Parse(input);

        // Assert
        await Assert.That(machines).IsEquivalentTo(expected);
    }

    [Test]
    public async Task GetNextCombosGetsDistinctCombosOfK()
    {
        // Arrange
        Machine machine = ExampleMachines[2];
        Button[] buttons = machine.Buttons;
        var expectedK1Combos = new List<List<Button>>
        {
            new() { buttons[0] },
            new() { buttons[1] },
            new() { buttons[2] },
            new() { buttons[3] },
        };
        var expectedK2Combos = new List<List<Button>>
        {
            new() { buttons[0], buttons[1] },
            new() { buttons[0], buttons[2] },
            new() { buttons[0], buttons[3] },
            new() { buttons[1], buttons[2] },
            new() { buttons[1], buttons[3] },
            new() { buttons[2], buttons[3] },
        };
        var expectedK3Combos = new List<List<Button>>
        {
            new() { buttons[0], buttons[1], buttons[2] },
            new() { buttons[0], buttons[1], buttons[3] },
            new() { buttons[0], buttons[2], buttons[3] },
            new() { buttons[1], buttons[2], buttons[3] },
        };
        var expectedK4Combos = new List<List<Button>>
        {
            new() { buttons[0], buttons[1], buttons[2], buttons[3] },
        };

        // Act
        IEnumerable<IEnumerable<Button>> k1Combos = ButtonOptimizer.GetNChooseKCombos(machine, 1);
        IEnumerable<IEnumerable<Button>> k2Combos = ButtonOptimizer.GetNChooseKCombos(machine, 2);
        IEnumerable<IEnumerable<Button>> k3Combos = ButtonOptimizer.GetNChooseKCombos(machine, 3);
        IEnumerable<IEnumerable<Button>> k4Combos = ButtonOptimizer.GetNChooseKCombos(machine, 4);

        // Assert
        await Assert.That(k1Combos).IsEquivalentTo(expectedK1Combos);
        await Assert.That(k2Combos).IsEquivalentTo(expectedK2Combos);
        await Assert.That(k3Combos).IsEquivalentTo(expectedK3Combos);
        await Assert.That(k4Combos).IsEquivalentTo(expectedK4Combos);
    }

    [Test]
    public async Task Part1ExampleProducesExpectedOutputForPart1()
    {
        // Arrange
        List<Machine> machines = ExampleMachines;
        const int expectedTotalButtonPresses = 7;
        var expectedButtonPresses = new List<int> { 2, 3, 2 };

        // Act
        List<int> resultPresses = ButtonOptimizer.FewestButtonPressesForMachines(machines);
        int sum = resultPresses.Sum();

        // Assert
        await Assert.That(resultPresses).IsEquivalentTo(expectedButtonPresses);
        await Assert.That(sum).IsEqualTo(expectedTotalButtonPresses);
    }
}