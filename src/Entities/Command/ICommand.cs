using FileSystem.Entities.FileSystems;
using FileSystem.Models.Results;

namespace FileSystem.Entities.Command;

public interface ICommand
{
    Status Execute(Context context);
}