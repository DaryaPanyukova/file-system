using FileSystem.Entities.Command;

namespace FileSystem.Services.Builders;

public interface ICommandBuilder
{
    ICommand Build();
}