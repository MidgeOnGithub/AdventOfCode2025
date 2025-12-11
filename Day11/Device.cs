namespace Day11;

public readonly record struct Device
{
    public Device(string Name, IEnumerable<string> OutDevices)
    {
        this.Name = Name;
        this.OutDevices = OutDevices.ToArray();
    }

    public string Name { get; }
    public string[] OutDevices { get; }

    public override string ToString() => $"Device: {Name} Outputs: {string.Join(", ", OutDevices)}";
}