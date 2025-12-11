namespace Day11;

public static class InputParser
{
    public static List<Device> Parse(string input)
    {
        string[] lines = input.Split(Environment.NewLine);

        List<Device> devices = [];
        foreach (string line in lines)
        {
            // Name is always the first 3 characters.
            string name = line[..3];

            // Each device is 3 characters long with a space between.
            string[] outputs = line[5..].Split(' ');
            var outDevices = new List<string>(outputs.Length);
            foreach (string output in outputs)
                outDevices.Add(output);

            devices.Add(new Device(name, outDevices));
        }

        return devices;
    }
}