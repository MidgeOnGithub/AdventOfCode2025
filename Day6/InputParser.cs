namespace Day6;

public static class InputParser
{
    public static IReadOnlyList<Problem> ParsePart1(string input)
    {
        string[] lines = input.Split('\n');

        // Get the number of columns in a row.
        int columnCount = lines[0]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Length;

        // Get lines' parts into a single enumerable structure.
        var rowParts = lines.Select(l => l.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)).ToList();
        for (var i = 0; i < lines.Length; i++)
            lines[i] = lines[i];

        // Extract problems by iterating over columns down the line parts.
        var problems = new List<Problem>();
        for (var column = 0; column < columnCount; column++)
        {
            var numbers = new List<ulong>();
            Operation? operation = null;

            foreach (string rowPart in rowParts.Select(linePart => linePart[column]))
                switch (rowPart)
                {
                    case "+":
                        operation = Operation.Add;
                        break;

                    case "*":
                        operation = Operation.Multiply;
                        break;

                    default:
                        numbers.Add(ulong.Parse(rowPart));
                        break;
                }

            if (numbers.Count == 0)
                throw new ArgumentException("Problem requires numbers");
            if (operation is null)
                throw new ArgumentException("Problem requires an operation");

            problems.Add(new Problem(numbers.AsReadOnly(), operation.Value));
        }

        return problems;
    }
}