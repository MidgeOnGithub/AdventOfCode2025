namespace Day10;

public readonly record struct Button(int Id, int[] Indices) : IEquatable<Button?>, IComparable<Button?>
{
    public bool Equals(Button? other) => Id == other?.Id;

    public int CompareTo(Button? other) => Id.CompareTo(other?.Id);
}