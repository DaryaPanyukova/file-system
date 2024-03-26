using FileSystem.Entities.Command;
using FileSystem.Models.Results;
using FileSystem.Services.Builders.File;
using FileSystem.Services.StringIterators;

namespace FileSystem.Entities.Parsers.ArgumentParser.FileShow;

public class FileShowModeParser : IArgumentParser<FileShowCommandBuilder>
{
    public IArgumentValueParser<FileShowCommandBuilder>? ModeValueParser { get; set; }
    public IArgumentParser<FileShowCommandBuilder>? NextArgumentParser { get; set; }

    public Result Parse(IStringIterator input, FileShowCommandBuilder builder)
    {
        string? flag = input.GetTerm();

        if (flag == "-m")
        {
            Status? status = ModeValueParser?.Parse(input, builder);
            if (status is Status.Fail)
            {
                return new Result(status, null);
            }
        }
        else if (flag != null && NextArgumentParser == null)
        {
            return new Result(new Status.Fail.SyntaxError.UnknownFlag(), null);
        }

        if (NextArgumentParser == null || flag == null)
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
        else
        {
            input.PutTerm();
            return NextArgumentParser.Parse(input, builder);
        }
    }
}