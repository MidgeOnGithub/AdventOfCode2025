namespace Day10;

public readonly record struct Machine(bool[] Lights, Button[] Buttons, int[] Joltages): IEquatable<Machine?>
{
    public bool Equals(Machine? other)
        => Lights.Equals(other?.Lights) && Buttons.SequenceEqual(other?.Buttons) && Joltages.Equals(other?.Joltages);

    public override int GetHashCode() => HashCode.Combine(Lights, Buttons, Joltages);
}