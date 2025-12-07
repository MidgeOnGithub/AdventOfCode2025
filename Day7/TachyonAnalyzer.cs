namespace Day7;

using TachyonManifold = IReadOnlyList<IReadOnlyList<Node>>;

public static class TachyonAnalyzer
{
    public static ulong CountSplits(TachyonManifold tm)
    {
        ulong totalSplits = 0;

        var manifoldWidth = tm[0].Count;
        var beamsFalling = new int[manifoldWidth];

        foreach (var row in tm)
        {
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
        }

        return totalSplits;
    }
}