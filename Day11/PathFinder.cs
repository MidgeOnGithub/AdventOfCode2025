[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Tests")]

namespace Day11;

public class PathFinder
{
    public static int FindPaths(IReadOnlyList<Device> devices)
    {
        Device startDevice = devices.Single(d => d.Name == "you");

        // Build a tree
        var root = new TreeNode(startDevice.Name);
        var ends = AddChildDevices(devices, startDevice, root);

        return ends;
    }

    internal static int AddChildDevices(IReadOnlyList<Device> allDevices, Device device, TreeNode parent, int ends = 0)
    {
        foreach (string name in device.OutDevices)
        {
            if (name == "out")
            {
                // End the path
                ends++;
                parent.AddChild(name);
                TreeNode? nextParent = parent;

                // Form path string.
                Stack<string> pathNodes = new();
                pathNodes.Push(name);
                while (nextParent is not null)
                {
                    pathNodes.Push(nextParent.DeviceName);
                    nextParent = nextParent.Parent;
                }
                string path = string.Join(" -> ", pathNodes);
                Console.WriteLine($"Found path: {path}");

                continue;
            }

            // Get the related device and recursively add its children.
            Device nextDevice = allDevices.Single(d => d.Name == name);
            TreeNode child = parent.AddChild(nextDevice.Name);
            ends += AddChildDevices(allDevices, nextDevice, child);
        }

        return ends;
    }
}