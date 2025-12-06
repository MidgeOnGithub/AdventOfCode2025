namespace Day6;

public record Problem(IReadOnlyList<ulong> Numbers, Operation Operation)
{
    public override string ToString() => $"{Operation} {string.Join(", ", Numbers)}";
}