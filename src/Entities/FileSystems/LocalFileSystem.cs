using FileSystem.Models;
using FileSystem.Models.Results;

namespace FileSystem.Entities.FileSystems;

public class LocalFileSystem : IFileSystem
{
    public Status Connect(string path)
    {
        if (Exists(path))
        {
            return new Status.Success();
        }

        return new Status.Fail.ExecutionError.NonExistentFile();
    }

    public Status Disconnect()
    {
        return new Status.Success();
    }

    public bool Exists(string path)
    {
        return File.Exists(path);
    }

    public StatusStringReturnType FileShow(string path)
    {
        try
        {
            string file = File.ReadAllText(path);
            return new StatusStringReturnType(new Status.Success(), file);
        }
        catch (FileNotFoundException)
        {
            return new StatusStringReturnType(new Status.Fail.ExecutionError.NonExistentFile(), string.Empty);
        }
    }

    public Status FileMove(string sourcePath, string destinationPath)
    {
        try
        {
            File.Move(sourcePath, destinationPath);
            return new Status.Success();
        }
        catch (FileNotFoundException)
        {
            return new Status.Fail.ExecutionError.NonExistentFile();
        }
        catch (IOException)
        {
            return new Status.Fail.ExecutionError.FilenameDuplication();
        }
    }

    public Status FileCopy(string sourcePath, string destinationPath)
    {
        try
        {
            File.Copy(sourcePath, destinationPath);
            return new Status.Success();
        }
        catch (FileNotFoundException)
        {
            return new Status.Fail.ExecutionError.NonExistentFile();
        }
        catch (IOException)
        {
            return new Status.Fail.ExecutionError.FilenameDuplication();
        }
    }

    public Status FileDelete(string path)
    {
        try
        {
            File.Delete(path);
            return new Status.Success();
        }
        catch (FileNotFoundException)
        {
            return new Status.Fail.ExecutionError.NonExistentFile();
        }
        catch (IOException)
        {
            return new Status.Fail.ExecutionError.DeletingFileCurrentlyInUse();
        }
    }

    public Status FileRename(string path, string name)
    {
        try
        {
            string? directory = Path.GetDirectoryName(path);
            string newPath = Path.Combine(directory ?? string.Empty, name);
            File.Move(path, newPath);
            return new Status.Success();
        }
        catch (FileNotFoundException)
        {
            return new Status.Fail.ExecutionError.NonExistentFile();
        }
        catch (IOException)
        {
            return new Status.Fail.ExecutionError.FilenameDuplication();
        }
    }

    public StatusStringReturnType GetFileName(string path)
    {
        try
        {
            string file = Path.GetFileName(path);
            return new StatusStringReturnType(new Status.Success(), file);
        }
        catch (FileNotFoundException)
        {
            return new StatusStringReturnType(new Status.Fail.ExecutionError.NonExistentFile(), string.Empty);
        }
    }

    public string[] GetFiles(string path)
    {
        return Directory.GetFiles(path);
    }

    public string[] GetDirectories(string path)
    {
        return Directory.GetDirectories(path);
    }
}