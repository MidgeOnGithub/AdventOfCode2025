namespace Day5;

public class ProduceFinder
{
    public ulong Count { get; private set; } = 0;

    public IReadOnlyList<ulong> FindUnspoiledFood(IReadOnlyList<(ulong lower, ulong upper)> freshRanges, IReadOnlyList<ulong> available)
    {
        var unspoiledFood = new List<ulong>();
        foreach (ulong ingredient in available)
        {
            if (!freshRanges.Any(range => ingredient >= range.lower && ingredient <= range.upper))
                continue;

            unspoiledFood.Add(ingredient);
            Count++;
            Console.WriteLine($"Found unspoiled ingredient ID {ingredient}; total found: {Count}.");
        }

        return unspoiledFood;
    }
}