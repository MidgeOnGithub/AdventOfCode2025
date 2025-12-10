namespace Day10;

public static class InputParser
{
    public static List<Machine> Parse(string input)
    {
        string[] lines = input.Split(Environment.NewLine);

        var machines = new List<Machine>(lines.Length);
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            bool[] lights = [];
            List<Button> buttons = [];
            int[] joltages = [];

            string[] parts = line.Split(' ');
            var buttonId = 0;
            foreach (string part in parts)
            {
                string inside = part[1..^1];
                switch (part[0])
                {
                    case '[':
                        lights = new bool[inside.Length];
                        for (var i = 0; i < inside.Length; i++)
                        {
                            char c = inside[i];
                            lights[i] = c switch
                            {
                                '.' => false,
                                '#' => true,
                                _   => throw new ArgumentException($"Invalid light character '{c}' in line '{line}'"),
                            };
                        }
                        break;

                    case '(':
                        string[] buttonParts = inside.Split(',');
                        var indices = new int[buttonParts.Length];
                        for (var i = 0; i < buttonParts.Length; i++)
                        {
                            string indexString = buttonParts[i];
                            indices[i] = int.Parse(indexString);
                        }

                        buttons.Add(new Button(buttonId, indices));
                        buttonId++;
                        break;

                    case '{':
                        string[] joltageParts = inside.Split(',');
                        joltages = new int[joltageParts.Length];
                        for (var i = 0; i < joltageParts.Length; i++)
                        {
                            string joltString = joltageParts[i];
                            joltages[i] = int.Parse(joltString);
                        }
                        break;

                    default:
                        throw new ArgumentException($"Invalid character '{part[0]}' in line '{line}'");
                }
            }

            var newMachine = new Machine(lights, buttons.ToArray(), joltages);
            machines.Add(newMachine);
        }

        return machines;
    }
}