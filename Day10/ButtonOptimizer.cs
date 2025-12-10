[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Tests")]

namespace Day10;

public class ButtonOptimizer
{
    private readonly struct State(List<Button> pressedButtons, bool[] lights)
    {
        public List<Button> PressedButtons { get; } = pressedButtons;
        public bool[] Lights { get; } = lights;
    }

    public static List<int> FewestButtonPressesForMachines(IReadOnlyList<Machine> machines)
    {
        var buttonPresses = new List<int>(machines.Count);
        buttonPresses.AddRange(machines.Select(FewestButtonPressesForMachine));
        return buttonPresses;
    }

    private static int FewestButtonPressesForMachine(Machine machine)
    {
        // Pushing the same button twice in any chain reverts its work in that state. So we only perform unique combos of button presses.
        // We're effectively checking C(n, k) and incrementing k. To reduce calculations, we store the states of k-1 and re-use for k.
        var states = new List<State>();

        for (var k = 1; k <= machine.Buttons.Length; k++)
        {
            IEnumerable<IEnumerable<Button>> combos = GetNChooseKCombos(machine, k);

            states = k == 1
                ? PerformFirstButtonPresses(machine)
                : PerformNextButtonPresses(combos, states);

            foreach (State s in states)
            {
                string buttonsString = string.Join('.', s.PressedButtons.Select(b => b.Id));

                if (!s.Lights.SequenceEqual(machine.Lights))
                    continue;

                Console.WriteLine($"Found solution with {k} button presses: {buttonsString}.");
                return k;
            }
        }

        throw new InvalidOperationException($"No solution found for machine {machine}.");
    }

    private static void ApplyButtonPressToIndicators(Button button, ref bool[] lights)
    {
        foreach (int i in button.Indices)
            lights[i] = !lights[i];
    }

    private static List<State> PerformFirstButtonPresses(Machine machine)
    {
        var states = new List<State>();

        foreach (Button button in machine.Buttons)
        {
            var indicators = new bool[machine.Lights.Length]; // For k = 1, start with all lights off.
            ApplyButtonPressToIndicators(button, ref indicators);
            states.Add(new State([button], indicators));
        }

        return states;
    }

    private static List<State> PerformNextButtonPresses(IEnumerable<IEnumerable<Button>> combos, List<State> states)
    {
        var newStates = new List<State>();
        var prevButtonCount = states[0].PressedButtons.Count;

        foreach (IEnumerable<Button> combo in combos)
        {
            List<Button> newCombo = combo.ToList();
            Button newButton = newCombo.Last();

            // There should be at least one previous state which matches k - 1 buttons.
            State state = states.First(s => s.PressedButtons.SequenceEqual(newCombo.Take(prevButtonCount)));

            // Update the lights with the new button press.
            bool[] lights = state.Lights.ToArray();
            ApplyButtonPressToIndicators(newButton, ref lights);

            // Track the new button combo's state.
            newStates.Add(new State(newCombo, lights));
        }

        return newStates;
    }

    /// <summary>An implementation of C(n, k), known as n choose k, getting only unique combinations and ignoring permutations.</summary>
    internal static IEnumerable<IEnumerable<Button>> GetNChooseKCombos(Machine machine, int k)
    {
        if (k == machine.Buttons.Length)
        {
            yield return machine.Buttons.AsEnumerable();
            yield break;
        }

        switch (k)
        {
            case < 1:
                throw new ArgumentOutOfRangeException(nameof(k));

            case 1:
                foreach (Button button in machine.Buttons)
                    yield return [button];
                yield break;

            case > 1:
                IEnumerable<IEnumerable<Button>> previousCombos = GetNChooseKCombos(machine, k - 1);

                IEnumerable<IEnumerable<Button>> newCombos = previousCombos
                    .SelectMany(
                        // For each combo, find the next higher buttons; the selector then flattens these into a combined enumerable.
                        // This avoids permutations since only the last element of each combo is compared.
                        collectionSelector: combo => machine.Buttons.Where(b => b.CompareTo(combo.Last()) > 0),
                        // Add that next higher button back to the combo as the final new combo.
                        resultSelector: (combo, button) => combo.Append(button)
                    );

                foreach (IEnumerable<Button> combo in newCombos)
                    yield return combo;

                yield break;
        }
    }
}