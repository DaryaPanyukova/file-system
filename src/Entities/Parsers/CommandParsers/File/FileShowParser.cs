using FileSystem.Entities.Command.File;
using FileSystem.Entities.Parsers.ArgumentParser;
using FileSystem.Models.Results;
using FileSystem.Services.Builders.File;
using FileSystem.Services.StringIterators;

namespace FileSystem.Entities.Parsers.CommandParsers.File;

public class FileShowParser : ICommandParser
{
    public ICommandParser? OtherCommandParser { get; set; }

    public IArgumentParser<FileShowCommandBuilder>? ArgumentParser { get; set; }

    public Result Parse(IStringIterator input)
    {
        string? word = input.GetTerm();

        if (word == null)
        {
            return new Result(new Status.Success.EmptyCommand(), null);
        }

        Result result;
        if (word == "show")
        {
            result = ArgumentParser == null
                ? new Result(new Status.Success(), new FileShowCommand(null, null))
                : ArgumentParser.Parse(input, new FileShowCommandBuilder());
        }
        else
        {
            input.PutTerm();
            result = OtherCommandParser == null
                ? new Result(new Status.Fail.SyntaxError.UnknownCommand(), null)
                : OtherCommandParser.Parse(input);
        }

        if (result.Status is Status.Success && !input.IsEnd())
        {
            result = new Result(new Status.Fail.SyntaxError.TooManyArguments(), null);
        }

        return result;
    }
}