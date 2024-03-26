using FileSystem.Entities.Command;
using FileSystem.Models.Results;
using FileSystem.Services.Builders.File;
using FileSystem.Services.StringIterators;

namespace FileSystem.Entities.Parsers.ArgumentParser.FileRename;

public class FileRenamePathParser : IArgumentParser<FileRenameCommandBuilder>
{
    public IArgumentParser<FileRenameCommandBuilder>? NextArgumentParser { get; set; }

    public Result Parse(IStringIterator input, FileRenameCommandBuilder builder)
    {
        string? path = input.GetTerm();
        builder.WithPath(path);
        if (path == null || NextArgumentParser == null)
        {
            try
            {
                ICommand command = builder.Build();
                return new Result(new Status.Success(), command);
            }
            catch (ArgumentNullException)
            {
                return new Result(new Status.Fail.SyntaxError.CommandNotInitialized(), null);
            }
        }

        return NextArgumentParser.Parse(input, builder);
    }
}