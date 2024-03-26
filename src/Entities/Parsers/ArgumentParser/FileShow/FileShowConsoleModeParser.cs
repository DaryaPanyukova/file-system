using FileSystem.Entities.Outputs;
using FileSystem.Models.Results;
using FileSystem.Services.Builders.File;
using FileSystem.Services.StringIterators;

namespace FileSystem.Entities.Parsers.ArgumentParser.FileShow;

public class FileShowConsoleModeParser : IArgumentValueParser<FileShowCommandBuilder>
{
    public IArgumentValueParser<FileShowCommandBuilder>? NextModeValueParser { get; set; }

    public Status Parse(IStringIterator input, FileShowCommandBuilder builder)
    {
        string? mode = input.GetTerm();
        if (mode == null)
        {
            return new Status.Fail.SyntaxError.CommandNotInitialized();
        }

        if (mode == "console")
        {
            builder.WithOutput(new ConsoleOutput());
            return new Status.Success();
        }
        else if (NextModeValueParser != null)
        {
            input.PutTerm();
            return NextModeValueParser.Parse(input, builder);
        }
        else
        {
            return new Status.Fail.SyntaxError.UnknownFlagValue();
        }
    }
}