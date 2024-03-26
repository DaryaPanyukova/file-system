using FileSystem.Entities.FileSystems;
using FileSystem.Models.Results;
using FileSystem.Services.Builders.Connect;
using FileSystem.Services.StringIterators;

namespace FileSystem.Entities.Parsers.ArgumentParser.Connect;

public class ConnectLocalModeParser : IArgumentValueParser<ConnectCommandBuilder>
{
    public IArgumentValueParser<ConnectCommandBuilder>? NextModeValueParser { get; set; }

    public Status Parse(IStringIterator input, ConnectCommandBuilder builder)
    {
        string? mode = input.GetTerm();
        if (mode == null)
        {
            return new Status.Fail.SyntaxError.CommandNotInitialized();
        }

        if (mode == "local")
        {
            builder.WithFileSystem(new LocalFileSystem());
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