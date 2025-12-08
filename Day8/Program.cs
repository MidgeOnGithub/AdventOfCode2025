using Day8;

string input = File.ReadAllText("input.txt");
var boxes = InputParser.Parse(input);
Console.WriteLine($"Number of boxes: {boxes.Count}");

const int connectionsToMake = 1000;
var finder = new CircuitFinder();
finder.CreateShortestCircuits(boxes, connectionsToMake);

// Part 1
var largestCircuits = finder.NLargestCircuits(3);
Console.WriteLine($"Largest circuit sizes from the first {connectionsToMake} connections: {string.Join(',', largestCircuits.Select(c => c.Size))}");
Console.WriteLine($"Largest circuit sizes multiplied: {finder.NLargestCircuitsSize(3)}");

// Part 2
(JunctionBox, JunctionBox) finalPairToConnectAllBoxes = finder.FindFinalConnectingPair(boxes);
Console.WriteLine($"Final pair to connect all boxes: {finalPairToConnectAllBoxes.Item1.Id} and {finalPairToConnectAllBoxes.Item2.Id}");
ulong finalNumber = (ulong) finalPairToConnectAllBoxes.Item1.X * (ulong) finalPairToConnectAllBoxes.Item2.X;
Console.WriteLine($"Final distance: {finalNumber}");