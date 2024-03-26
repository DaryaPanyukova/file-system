using FileSystem.Entities.FileSystems;
using FileSystem.Entities.Outputs;
using FileSystem.Models;
using FileSystem.Models.Results;
using FileSystem.Services.Builders.File;

namespace FileSystem.Entities.Command.File;

public class FileShowCommand : ICommand
{
    public FileShowCommand(string? path, IOutput? output)
    {
        Path = path ?? throw new ArgumentNullException(nameof(path));
        Output = output ?? throw new ArgumentNullException(nameof(output));
    }

    public static FileShowCommandBuilder Builder => new FileShowCommandBuilder();

    public string Path { get; private set; }
    public IOutput Output { get; private set; }

    public Status Execute(Context context)
    {
        StatusStringReturnType result = context.FileShow(Path);
        if (result.Status is Status.Success)
        {
            Output.Print(result.File);
        }

        return result.Status;
    }
}