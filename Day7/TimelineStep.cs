namespace Day7;

public readonly record struct TimelineStep(Direction Direction, int Row)
{
    public override int GetHashCode() => HashCode.Combine((int) Direction, Row);
}