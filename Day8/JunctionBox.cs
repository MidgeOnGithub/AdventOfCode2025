namespace Day8;

public readonly record struct JunctionBox(int Id, int X, int Y, int Z)
{
    public float DistanceTo(JunctionBox other)
    {
        int xDiff = X - other.X;
        int yDiff = Y - other.Y;
        int zDiff = Z - other.Z;
        return MathF.Sqrt(MathF.Pow(xDiff, 2) + MathF.Pow(yDiff, 2) + MathF.Pow(zDiff, 2));
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}