using FileSystem.Models;
using FileSystem.Models.Results;

namespace FileSystem.Entities.FileSystems;

public class Context
{
    public IFileSystem? FileSystem { get; private set; }
    public string Directory { get; private set; } = string.Empty;

    public Status Connect(string address, IFileSystem fileSystem)
    {
        Directory = address;
        FileSystem = fileSystem;
        return fileSystem.Connect(address);
    }

    public Status Disconnect()
    {
        if (FileSystem == null)
        {
            return new Status.Fail.ExecutionError.NoConnectionToFileSystem();
        }

        Status status = FileSystem.Disconnect();
        FileSystem = null;
        Directory = string.Empty;
        return status;
    }

    public Status TreeGoto(string path)
    {
        Directory = MakeAbsolutePath(path);
        if (FileSystem == null)
        {
            return new Status.Fail.ExecutionError.NoConnectionToFileSystem();
        }

        return FileSystem.Exists(Directory)
            ? new Status.Success()
            : new Status.Fail.ExecutionError.NonExistentFile();
    }

    public StatusStringReturnType FileShow(string path)
    {
        if (FileSystem == null)
        {
            return new StatusStringReturnType(new Status.Fail.ExecutionError.NoConnectionToFileSystem(), string.Empty);
        }

        return FileSystem.FileShow(MakeAbsolutePath(path));
    }

    public Status FileMove(string sourcePath, string destinationPath)
    {
        return FileSystem?.FileMove(MakeAbsolutePath(sourcePath), MakeAbsolutePath(destinationPath)) ??
               new Status.Fail.ExecutionError.NoConnectionToFileSystem();
    }

    public Status FileCopy(string sourcePath, string destinationPath)
    {
        return FileSystem?.FileCopy(MakeAbsolutePath(sourcePath), MakeAbsolutePath(destinationPath)) ??
               new Status.Fail.ExecutionError.NoConnectionToFileSystem();
    }

    public Status FileDelete(string path)
    {
        return FileSystem?.FileDelete(MakeAbsolutePath(path)) ??
               new Status.Fail.ExecutionError.NoConnectionToFileSystem();
    }

    public Status FileRename(string path, string name)
    {
        return FileSystem?.FileRename(MakeAbsolutePath(path), name) ??
               new Status.Fail.ExecutionError.NoConnectionToFileSystem();
    }

    private string MakeAbsolutePath(string path)
    {
        return Path.IsPathRooted(path)
            ? path
            : Path.Combine(Directory, path);
    }
}