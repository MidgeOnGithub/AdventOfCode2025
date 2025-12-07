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
        var rowParts = lines
            .Select(l => l.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            .ToList();

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

    public static IReadOnlyList<Problem> ParsePart2(string input)
    {
        string[] lines = input.Split(Environment.NewLine);

        // Get the number of columns in a row.
        int problemOperationsCount = lines[0]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Length;

        // Get the width of each column by finding where the operators are; they are the character on the far left of each column.
        string operationsLine = lines.Last();
        var columnWidths = new List<int>(problemOperationsCount);
        int previousOperatorIndex = -2;
        foreach ((char c, int i) charIndex in operationsLine.Reverse().Select((c, i) => (c, i)))
        {
            if (charIndex.c is not ('+' or '*'))
                continue;

            // Subtract 1 for the space between columns.
            // For the first right-to-left column, it works because we initialized the previous index as -2 to account for the lack of space.
            columnWidths.Add(charIndex.i - previousOperatorIndex - 1);
            previousOperatorIndex = charIndex.i;
        }

        // Get lines' parts into a single enumerable structure.
        List<List<string>> rightToLeftParts = new(lines.Length);
        foreach (string line in lines)
        {
            var lineRightToLeft = new string(line.Reverse().ToArray());

            var parts = new List<string>(problemOperationsCount);
            var previousIndex = 0;
            foreach (int width in columnWidths)
            {
                parts.Add(lineRightToLeft[previousIndex..(previousIndex + width)]);
                previousIndex += width + 1; // Account for the space between columns.
            }

            rightToLeftParts.Add(parts);
        }

        // Extract problems by reading problem columns.
        var problems = new List<Problem>();
        for (var problemColumn = 0; problemColumn < problemOperationsCount; problemColumn++)
        {
            var numbers = new List<ulong>();
            Operation? operation = null;

            var numberStringsToProcess = new List<string>();
            foreach (string rowPart in rightToLeftParts.Select(linePart => linePart[problemColumn]))
                if (rowPart.Contains("+"))
                    operation = Operation.Add;
                else if (rowPart.Contains("*"))
                    operation = Operation.Multiply;
                else
                    numberStringsToProcess.Add(rowPart);

            // Process the number strings; they're already in right-to-left, so iterate normally.
            int columnWidth = columnWidths[problemColumn];
            for (var digitColumn = 0; digitColumn < columnWidth; digitColumn++)
            {
                List<char> digitChars = [];
                foreach (string s in numberStringsToProcess)
                    if (s[digitColumn] is not ' ')
                        digitChars.Add(s[digitColumn]);

                ulong resultingNumber = 0;
                for (var i = 0; i < digitChars.Count; i++)
                    resultingNumber += ulong.Parse(digitChars[i].ToString()) * (ulong) Math.Pow(10, digitChars.Count - i - 1);

                numbers.Add(resultingNumber);
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