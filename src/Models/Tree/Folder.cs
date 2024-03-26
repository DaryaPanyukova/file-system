using System.Collections.ObjectModel;
using FileSystem.Services.Visitors;

namespace FileSystem.Models.Tree;

public record Folder : IDirectoryTreeNode
{
    public Folder(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }
    public Collection<IDirectoryTreeNode> Files { get; } = new Collection<IDirectoryTreeNode>();

    public void AddNode(IDirectoryTreeNode node)
    {
        Files.Add(node);
    }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}