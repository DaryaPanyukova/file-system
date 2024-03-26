using FileSystem.Entities.Command;
using FileSystem.Models.Results;
using FileSystem.Services.Builders.File;
using FileSystem.Services.StringIterators;

namespace FileSystem.Entities.Parsers.ArgumentParser.FileMove;

public class FileMoveSourcePathParser : IArgumentParser<FileMoveCommandBuilder>
{
    public IArgumentParser<FileMoveCommandBuilder>? NextArgumentParser { get; set; }

    public Result Parse(IStringIterator input, FileMoveCommandBuilder builder)
    {
        string? path = input.GetTerm();
        builder.WithSourcePath(path);
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