using System.Collections;

namespace Day11;

public class TreeNode(string deviceName) : IEnumerable<TreeNode>
{
    public string DeviceName { get; set; } = deviceName;
    public TreeNode? Parent { get; set; }
    public LinkedList<TreeNode> Children { get; } = [];

    public int Level => Parent?.Level + 1 ?? 0;

    public TreeNode AddChild(string deviceName)
    {
        var childNode = new TreeNode(deviceName) { Parent = this, };
        Children.AddLast(childNode);
        return childNode;
    }

    public IEnumerator<TreeNode> GetEnumerator()
    {
        yield return this;

        foreach (TreeNode child in Children.SelectMany(child => child))
            yield return child;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}