using FileSystem.Entities.Command;
using FileSystem.Models.Results;
using FileSystem.Services.Builders.File;
using FileSystem.Services.StringIterators;

namespace FileSystem.Entities.Parsers.ArgumentParser.FileCopy;

public class FileCopyDestinationPathParser : IArgumentParser<FileCopyCommandBuilder>
{
    public IArgumentParser<FileCopyCommandBuilder>? NextArgumentParser { get; set; }

    public Result Parse(IStringIterator input, FileCopyCommandBuilder builder)
    {
        string? path = input.GetTerm();
        builder.WithDestinationPath(path);
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