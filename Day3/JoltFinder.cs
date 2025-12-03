namespace Day3;

public class JoltFinder
{
    public ulong Sum { get; private set; }

    public IReadOnlyList<IReadOnlyList<uint>> ProcessBanks(IReadOnlyList<IReadOnlyList<uint>> banks)
    {
        var highestJolts = new List<IReadOnlyList<uint>>(banks.Count);
        highestJolts.AddRange(banks.Select(FindHighestJolt));

        return highestJolts;
    }

    private IReadOnlyList<uint> FindHighestJolt(IReadOnlyList<uint> bank)
    {
        uint firstDigit = 0;
        uint secondDigit = 0;

        for (var i = 0; i < bank.Count; i++)
        {
            uint battery = bank[i];

            // Upgrade the first digit only if at least one more battery is in the bank.
            if (battery > firstDigit && i != bank.Count - 1)
            {
                firstDigit = battery;
                secondDigit = 0;
                continue;
            }

            // Upgrade the second digit whenever possible.
            if (battery > secondDigit)
                secondDigit = battery;
        }

        uint jolt = secondDigit + 10 * firstDigit;
        Sum += jolt;

        return [ firstDigit, secondDigit ];
    }
}