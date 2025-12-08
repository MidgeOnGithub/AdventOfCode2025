namespace Day8;

public readonly record struct Circuit() : IEquatable<Circuit?>
{
    private readonly HashSet<JunctionBox> _junctionBoxes = [];

    public int Size => _junctionBoxes?.Count ?? 0;

    public void Add(JunctionBox junctionBox) => _junctionBoxes.Add(junctionBox);

    public bool Contains(JunctionBox junctionBox) => _junctionBoxes.Contains(junctionBox);

    public void IntegrateOther(Circuit? other)
    {
        if (other is null)
            return;

        foreach (JunctionBox otherBox in other.Value._junctionBoxes)
            Add(otherBox);
    }

    public bool Equals(Circuit? other)
    {
        if (other is null)
            return false;

        return other.Value.Size == Size && _junctionBoxes.SetEquals(other.Value._junctionBoxes);
    }
}