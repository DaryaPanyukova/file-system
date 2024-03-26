using FileSystem.Entities.FileSystems;
using FileSystem.Models.Tree;

namespace FileSystem.Entities.DirectoryTrees;

public class DirectoryTreeBuilder
{
    public DirectoryTreeBuilder(Context context)
    {
        Context = context;
    }

    public Context Context { get; private set; }

    public DirectoryTree Build(int depth)
    {
        var tree = new DirectoryTree();
        if (Context.FileSystem == null)
        {
            return tree;
        }

        Visit(Context.Directory, Context.FileSystem, depth, tree);
        return tree;
    }

    private void Visit(string path, IFileSystem fileSystem, int depth, DirectoryTree tree)
    {
        if (depth == 0)
        {
            return;
        }

        if (fileSystem.Exists(path))
        {
            string fileName = fileSystem.GetFileName(path).File;
            tree.AddFile(fileName);
        }
        else if (fileSystem.Exists(path))
        {
            string directoryName = fileSystem.GetFileName(path).File;
            Folder folder = tree.AddFolder(directoryName);

            foreach (string file in fileSystem.GetFiles(path))
            {
                string fileName = fileSystem.GetFileName(file).File;
                var node = new FileNode(fileName);
                folder.AddNode(node);
            }

            foreach (string directory in fileSystem.GetDirectories(path))
            {
                Visit(directory, fileSystem, depth - 1, tree);
            }
        }
    }
}