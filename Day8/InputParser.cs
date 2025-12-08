namespace Day8;

public static class InputParser
{
    public static IReadOnlyList<JunctionBox> Parse(string input)
    {
        var junctionBoxes = new List<JunctionBox>();

        string[] lines = input.Split(Environment.NewLine);
        for (var i = 0; i < lines.Length; i++)
        {
            string line = lines[i];

            string[] parts = line.Split(',');
            if (parts.Length != 3)
                throw new ArgumentException("Lines should have 3 points.");

            int x = int.Parse(parts[0]);
            int y = int.Parse(parts[1]);
            int z = int.Parse(parts[2]);
            var junctionBox = new JunctionBox(i, x, y, z);

            junctionBoxes.Add(junctionBox);
        }

        return junctionBoxes;
    }
}