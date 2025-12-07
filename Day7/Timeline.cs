namespace Day7;

public sealed class Timeline(List<TimelineStep> timelineSteps)
{
    public readonly List<TimelineStep> TimelineSteps = timelineSteps;

    public Timeline Clone() =>
        new(TimelineSteps
            .Select(step => step) // clones each record
            .ToList());

    public void Add(TimelineStep step) => TimelineSteps.Add(step);

    public bool Equals(Timeline? other) => other?.TimelineSteps != null && TimelineSteps.SequenceEqual(other.TimelineSteps);

    public override int GetHashCode()
    {
        var hash = new HashCode();
        foreach (TimelineStep step in TimelineSteps)
            hash.Add(step);
        return hash.ToHashCode();
    }
}