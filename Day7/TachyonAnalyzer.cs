namespace Day7;

using TachyonManifold = IReadOnlyList<IReadOnlyList<Node>>;

public static class TachyonAnalyzer
{
    public static ulong CountSplits(TachyonManifold tm)
    {
        ulong totalSplits = 0;

        int manifoldWidth = tm[0].Count;
        var beamsFalling = new int[manifoldWidth];

        foreach (var row in tm)
            for (var i = 0; i < row.Count; i++)
            {
                Node node = row[i];

                switch (node)
                {
                    case Node.Empty:
                        continue;

                    case Node.Start:
                        beamsFalling[i] = 1;
                        continue;

                    case Node.Splitter:
                        bool beamCollision = beamsFalling[i] > 0;
                        if (beamCollision)
                        {
                            totalSplits++;

                            // Stop the current beam path.
                            beamsFalling[i] = 0;

                            // Split the beam path.
                            if (i != 0)
                                beamsFalling[i - 1] += 1;
                            if (i != manifoldWidth - 1)
                                beamsFalling[i + 1] += 1;
                        }
                        continue;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

        return totalSplits;
    }

    public static ulong CountTimelines(TachyonManifold tm)
    {
        // Pre-analyze to find the start in row 1, which is guaranteed by the inputs.
        int manifoldWidth = tm[0].Count;
        var beamColumn = 0;
        for (var i = 0; i < tm[0].Count; i++)
        {
            Node node = tm[0][i];
            if (node is not Node.Start)
                continue;

            beamColumn = i;
            break;
        }

        const int startingRow = 2; // We already found the start, and row 2 will be empty for beam travel.
        var processedSplitters = new HashSet<(int row, int column, ulong timelines)>();
        ulong totalTimelines = 0;
        ulong pathTimelines = 0;
        var timeline = new Timeline([]);
        ProcessBeamPath(tm, beamColumn, manifoldWidth, timeline, startingRow, processedSplitters, ref totalTimelines, ref pathTimelines);

        return totalTimelines;
    }

    /// <summary>Recursively analyzes the beam paths starting at the given row and column, going left first, then right.</summary>
    /// <remarks>When a splitter is revisited in another path, the stored result is re-used to avoid duplicate analysis.</remarks>
    private static void ProcessBeamPath(
        TachyonManifold tm, int beamColumn, int manifoldWidth, Timeline timeline, int startingRow,
        HashSet<(int row, int column, ulong timelines)> processedSplitters, ref ulong totalTimelines, ref ulong pathTimelines
    )
    {
        for (int rowNumber = startingRow; rowNumber < tm.Count; rowNumber++)
        {
            var rowNodes = tm[rowNumber];
            for (var column = 0; column < rowNodes.Count; column++)
            {
                Node node = rowNodes[column];

                switch (node)
                {
                    case Node.Empty:
                        continue;

                    case Node.Splitter:
                        bool beamCollision = beamColumn == column;
                        if (beamCollision)
                        {
                            // Re-use the results if this splitter was previously analyzed.
                            (int row, int column, ulong timelines) previousResult = processedSplitters
                                .FirstOrDefault(n => n.row == rowNumber && n.column == column);
                            if (previousResult is not (0, 0, 0))
                            {
                                totalTimelines += previousResult.timelines;
                                pathTimelines += previousResult.timelines;
                                Console.WriteLine($"Known {previousResult.timelines} timelines for ({rowNumber},{column}). Total is {totalTimelines}");
                                return;
                            };

                            // Perform the left timeline.
                            ulong leftTimelines = 0;
                            if (column != 0)
                            {
                                beamColumn = column - 1;
                                Timeline leftTimeline = timeline.Clone();
                                leftTimeline.Add(new TimelineStep(Direction.Left, rowNumber));
                                ProcessBeamPath(
                                    tm, beamColumn, manifoldWidth, leftTimeline, rowNumber + 2, processedSplitters,
                                    ref totalTimelines, ref leftTimelines
                                );
                            }

                            // Perform the right timeline.
                            ulong rightTimelines = 0;
                            if (column != manifoldWidth - 1)
                            {
                                beamColumn = column + 1;
                                Timeline rightTimeline = timeline.Clone();
                                rightTimeline.Add(new TimelineStep(Direction.Right, rowNumber));
                                ProcessBeamPath(
                                    tm, beamColumn, manifoldWidth, rightTimeline, rowNumber + 2, processedSplitters,
                                    ref totalTimelines, ref rightTimelines
                                );
                            }

                            // Combine the two timelines results.
                            ulong nodeTimelines = leftTimelines + rightTimelines;
                            pathTimelines += nodeTimelines;
                            processedSplitters.Add((rowNumber, column, nodeTimelines));
                            Console.WriteLine($"Found {nodeTimelines} timelines for ({rowNumber},{column}). Total is {totalTimelines}.");

                            return; // Splitter analysis complete.
                        }

                        continue;

                    case Node.Start: // Only one start is given.
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        // No more splitters, so this timeline is complete.
        pathTimelines++;
        totalTimelines++;
    }
}