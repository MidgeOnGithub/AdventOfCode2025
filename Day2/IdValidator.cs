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
            if (length < 2) // Pattern cannot be repeated twice in a 1-digit number.
                continue;

            // Find all divisions of the number as a string.
            var factors = new HashSet<int>();
            for (var x = 1; x <= length / 2; x++)
                if (length % x == 0)
                    factors.Add(x);

            // Through each possible division, check if all substrings match.
            var valid = true;
            foreach (int factor in factors)
            {
                string compareString = digits[..factor];

                var match = true;
                int skip = factor;
                while (skip <= length - factor)
                {
                    if (digits.Substring(skip, factor) != compareString)
                    {
                        // If the substring doesn't match, the pattern appears valid from this possible division.
                        match = false;
                        break;
                    }
                    skip += factor;
                }

                if (!match)
                    continue;

                valid = false;
                break;
            }

            if (valid)
                continue;

            invalidIds.Add(i);
            Sum += i;
        }

        return invalidIds;
    }
}