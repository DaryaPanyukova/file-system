using FileSystem.Models.Tree;

namespace FileSystem.Services.Visitors;

public interface IVisitor
{
    void Visit(Folder folder);
    void Visit(FileNode file);
}