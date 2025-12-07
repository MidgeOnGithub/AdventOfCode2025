namespace Day7;

using TachyonManifold = IReadOnlyList<IReadOnlyList<Node>>;

public static class InputParser
{
    public static TachyonManifold Parse(string input)
    {
        string[] lines = input.Split(Environment.NewLine);

        var nodes = new List<IReadOnlyList<Node>>(lines.Length);
        foreach (string line in lines)
        {
            var lineNodes = new List<Node>(line.Length);
            foreach (char c in line)
            {
                switch (c)
                {
                    case '.':
                        lineNodes.Add(Node.Empty);
                        break;

                    case 'S':
                        lineNodes.Add(Node.Start);
                        break;

                    case '^':
                        lineNodes.Add(Node.Splitter);
                        break;

                    default:
                        throw new ArgumentException($"Invalid character '{c}' in line '{line}'");
                }
            }

            nodes.Add(lineNodes.AsReadOnly());
        }

        return nodes;
    }
}