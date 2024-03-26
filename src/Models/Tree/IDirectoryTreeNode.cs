using FileSystem.Services.Visitors;

namespace FileSystem.Models.Tree;

public interface IDirectoryTreeNode
{
    void Accept(IVisitor visitor);
}