namespace Day5;

public class ProduceFinder
{
    public ulong UnspoiledIngredientsCount { get; private set; } = 0;

    public ulong CountPossibleIngredients(IReadOnlyList<(ulong upper, ulong lower)> freshRanges)
    {
        var uniqueRanges = new List<(ulong lower, ulong upper)>();
        foreach ((ulong lower, ulong upper) range in freshRanges)
            ExtendOrAddRange(uniqueRanges, range);

        uniqueRanges.Sort((a, b) => a.lower.CompareTo(b.lower));
        Console.WriteLine($"Final ranges: {string.Join(Environment.NewLine, uniqueRanges)}");

        return uniqueRanges.Aggregate<(ulong lower, ulong upper), ulong>(0, (sum, range) => sum + range.upper - range.lower + 1);
    }

    private static void ExtendOrAddRange(List<(ulong lower, ulong upper)> uniqueRanges, (ulong lower, ulong upper) newRange)
    {
        // Completely encompassed by an existing range.
        if (uniqueRanges.Any(p => p.lower <= newRange.lower && p.upper >= newRange.upper))
            return;

        // Find existing ranges totally encompassed by the new range.
        IEnumerable<(ulong lower, ulong upper)> overlappedBoth = uniqueRanges.Where(u => u.lower >= newRange.lower && u.upper <= newRange.upper).ToList();
        if (overlappedBoth.Any())
        {
            // Remove inferior ranges and replace with the new range.
            foreach ((ulong lower, ulong upper) overlappedRange in overlappedBoth)
            {
                uniqueRanges.RemoveAll(u => u.lower == overlappedRange.lower && u.upper == overlappedRange.upper);
                Console.WriteLine($"Inferior range {overlappedRange.lower}-{overlappedRange.upper} removed.");
            }

            uniqueRanges.Add(newRange);
            Console.WriteLine($"Inferior range removal completed, added {newRange.lower}-{newRange.upper}.");
        }

        // Find a range overlapped to the right by the new range.
        (ulong lower, ulong upper) overlappedRight = uniqueRanges.FirstOrDefault(u => u.lower <= newRange.lower && u.upper >= newRange.lower && u.upper <= newRange.upper);
        if (overlappedRight is not (0, 0))
        {
            // Extend encompassed range to include higher number from this range.
            uniqueRanges.RemoveAll(u => u.lower == overlappedRight.lower && u.upper == overlappedRight.upper);
            uniqueRanges.RemoveAll(u => u.lower == newRange.lower        && u.upper == newRange.upper);

            newRange.lower = overlappedRight.lower;
            uniqueRanges.Add((newRange.lower, newRange.upper));

            Console.WriteLine($"Overlapped range {overlappedRight.lower}-{overlappedRight.upper} extended right to become {newRange.lower}-{newRange.upper}.");
        }

        // Find a range overlapped to the left by the new range.
        (ulong lower, ulong upper) overlapsLeft = uniqueRanges.FirstOrDefault(u => u.lower >= newRange.lower && u.lower <= newRange.upper && u.upper >= newRange.upper);
        if (overlapsLeft is not (0, 0))
        {
            // Extend encompassed range to include lower number from this range.
            uniqueRanges.RemoveAll(u => u.lower == overlapsLeft.lower   && u.upper == overlapsLeft.upper);
            uniqueRanges.RemoveAll(u => u.lower == newRange.lower       && u.upper == newRange.upper);

            newRange.upper = overlapsLeft.upper;
            uniqueRanges.Add((newRange.lower, newRange.upper));

            Console.WriteLine($"Overlapped range {overlapsLeft.lower}-{overlapsLeft.upper} extended left to become {newRange.lower}-{newRange.upper}.");
        }

        // No overlaps processed, add the new range.
        if (!uniqueRanges.Any(u => u.lower == newRange.lower && u.upper == newRange.upper))
        {
            uniqueRanges.Add(newRange);

            Console.WriteLine($"New range {newRange.lower}-{newRange.upper} added.");
        }
    }

    public IReadOnlyList<ulong> FindUnspoiledFood(IReadOnlyList<(ulong lower, ulong upper)> freshRanges, IReadOnlyList<ulong> available)
    {
        var unspoiledFood = new List<ulong>();
        foreach (ulong ingredient in available)
        {
            if (!freshRanges.Any(range => ingredient >= range.lower && ingredient <= range.upper))
                continue;

            unspoiledFood.Add(ingredient);
            UnspoiledIngredientsCount++;
            Console.WriteLine($"Found unspoiled ingredient ID {ingredient}; total found: {UnspoiledIngredientsCount}.");
        }

        return unspoiledFood;
    }
}