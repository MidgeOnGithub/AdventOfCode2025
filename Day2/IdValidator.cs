namespace Day2;

public class IdValidator
{
    public ulong Sum { get; private set; }

    public IReadOnlyList<uint> FindInvalidIds(uint lower, uint upper)
    {
        var invalidIds = new List<uint>();

        for (uint i = lower; i <= upper; i++)
        {
            var digits = i.ToString();
            int length = digits.Length;
            if (length % 2 != 0) // Odd-length numbers cannot have a twice-repeated pattern as defined by the puzzle.
                continue;

            int half = length / 2;
            if (digits[..half] != digits[half..])
                continue;

            invalidIds.Add(i);
            Sum += i;
        }

        return invalidIds;
    }
}