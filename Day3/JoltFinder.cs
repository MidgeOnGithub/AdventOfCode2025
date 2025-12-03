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
        const int selectionCapacity = 12;
        var selected = new List<uint>(new uint[selectionCapacity]); // Initialize with zeroes.

        // Search through the bank.
        for (var i = 0; i < bank.Count; i++)
        {
            uint battery = bank[i];
            int remainingBatteries = bank.Count - 1 - i;

            // We can only edit our selections where there are enough batteries left to fill them.
            int start = Math.Max(0, selectionCapacity - 1 - remainingBatteries);
            for (int j = start; j < 12; j++)
            {
                if (battery <= selected[j])
                    continue;

                selected[j] = battery;

                // Re-zero the remaining selections.
                for (int k = j + 1; k < 12; k++)
                    selected[k] = 0;

                break;
            }
        }

        // Sum the selected batteries as if they were a large number.
        ulong jolt = 0;
        for (int i = selectionCapacity - 1; i >= 0; i--)
        {
            uint battery = selected[i];

            // Since we're iterating in reverse, each processed battery's magnitude is increased.
            double digitAdjustment = Math.Pow(10, selectionCapacity - 1 - i);
            jolt += (ulong) (battery * digitAdjustment);
        }

        Sum += jolt;
        return selected;
    }
}