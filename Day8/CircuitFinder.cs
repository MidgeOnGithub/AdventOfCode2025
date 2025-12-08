namespace Day8;

using BoxPairDistance = (JunctionBox box1, JunctionBox box2, float distance);

public class CircuitFinder()
{
    private readonly List<Circuit> _circuits = [];

    public IReadOnlyList<Circuit> NLargestCircuits(int n)
    {
        _circuits.Sort(new SizeComparer());
        _circuits.Reverse();
        return _circuits.Take(n).ToList();
    }

    public ulong NLargestCircuitsSize(int n)
    {
        _circuits.Sort(new SizeComparer());
        _circuits.Reverse();
        return _circuits.Take(n).Aggregate(1UL, (acc, c) => acc * (ulong) c.Size);
    }

    public void CreateShortestCircuits(IReadOnlyList<JunctionBox> junctionBoxes, int connectionsToMake)
    {
        _circuits.Clear();
        var pairDistances = GetPairDistances(junctionBoxes);

        var connectionsMade = 0;
        foreach ((JunctionBox box1, JunctionBox box2, float _) in pairDistances)
        {
            if (connectionsMade >= connectionsToMake)
                break; // We've found enough circuits.

            CreateOrMergeCircuits(box1, box2, ref connectionsMade);
        }
    }

    public (JunctionBox, JunctionBox) FindFinalConnectingPair(IReadOnlyList<JunctionBox> junctionBoxes)
    {
        _circuits.Clear();
        var pairDistances = GetPairDistances(junctionBoxes);

        var connectionsMade = 0;
        foreach ((JunctionBox box1, JunctionBox box2, float _) in pairDistances)
        {
            CreateOrMergeCircuits(box1, box2, ref connectionsMade);

            bool allBoxesInCircuit = _circuits.Count == 1 && _circuits[0].Size == junctionBoxes.Count;
            if (allBoxesInCircuit)
                return (box1, box2);
        }

        throw new Exception("No final connecting pair found.");
    }

    private static SortedSet<BoxPairDistance> GetPairDistances(IReadOnlyList<JunctionBox> junctionBoxes)
    {
        // Determine distances between pairs of boxes.
        var pairDistances = new SortedSet<BoxPairDistance>(new DistanceComparer());
        for (var i = 0; i < junctionBoxes.Count; i++)
        {
            JunctionBox box1 = junctionBoxes[i];
            for (var j = 0; j < junctionBoxes.Count; j++)
            {
                if (i == j)
                    continue;

                JunctionBox box2 = junctionBoxes[j];

                var pairDistance = new BoxPairDistance(box1, box2, box1.DistanceTo(box2));
                pairDistances.Add(pairDistance);
            }
        }

        return pairDistances;
    }

    private void CreateOrMergeCircuits(JunctionBox box1, JunctionBox box2, ref int connectionsMade)
    {
        Circuit box1Circuit = _circuits.SingleOrDefault(c => c.Contains(box1)); // Throwing with Single shows us a logical error.
        Circuit box2Circuit = _circuits.SingleOrDefault(c => c.Contains(box2)); // Throwing with Single shows us a logical error.

        // Neither box is in a circuit yet.
        if (box1Circuit.Size == 0 && box2Circuit.Size == 0)
        {
            var newCircuit = new Circuit();
            newCircuit.Add(box1);
            newCircuit.Add(box2);

            _circuits.Add(newCircuit);
            connectionsMade++;
            Console.WriteLine($"Created circuit with boxes {box1.Id} and {box2.Id}. Connections made: {connectionsMade}.");
            return;
        }

        // Both boxes are in a circuit.
        if (box1Circuit.Size != 0 && box2Circuit.Size != 0)
        {
            // If they're already connected, there's no work to do.
            if (box1Circuit == box2Circuit)
            {
                connectionsMade++;
                Console.WriteLine($"Boxes {box1.Id} and {box2.Id} were already connected. Connections made: {connectionsMade}.");
                return;
            }

            // Merge the circuits.
            box1Circuit.IntegrateOther(box2Circuit);
            _circuits.Remove(box2Circuit);
            connectionsMade++;
            Console.WriteLine($"Merged circuits for boxes {box1.Id} and {box2.Id}. Connections made: {connectionsMade}.");
            return;
        }

        // Only box 1 is in a circuit.
        if (box1Circuit.Size != 0 && box2Circuit.Size == 0)
        {
            box1Circuit.Add(box2);
            connectionsMade++;
            Console.WriteLine($"Added box {box2.Id} to circuit for box {box1.Id}. Connections made: {connectionsMade}.");
            return;
        }

        // Only box 2 is in a circuit.
        if (box1Circuit.Size == 0 && box2Circuit.Size != 0)
        {
            box2Circuit.Add(box1);
            connectionsMade++;
            Console.WriteLine($"Added box {box1.Id} to circuit for box {box2.Id}. Connections made: {connectionsMade}.");
        }
    }

    private class DistanceComparer : IComparer<BoxPairDistance>
    {
        public int Compare(BoxPairDistance x, BoxPairDistance y) => x.distance.CompareTo(y.distance);
    }

    private class SizeComparer : IComparer<Circuit>
    {
        public int Compare(Circuit x, Circuit y) => x.Size.CompareTo(y.Size);
    }
}