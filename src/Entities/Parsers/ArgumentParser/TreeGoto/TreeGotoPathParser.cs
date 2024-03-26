using FileSystem.Entities.Command;
using FileSystem.Models.Results;
using FileSystem.Services.Builders.Tree;
using FileSystem.Services.StringIterators;

namespace FileSystem.Entities.Parsers.ArgumentParser.TreeGoto;

public class TreeGotoPathParser : IArgumentParser<TreeGotoCommandBuilder>
{
    public IArgumentParser<TreeGotoCommandBuilder>? NextArgumentParser { get; set; }

    public Result Parse(IStringIterator input, TreeGotoCommandBuilder builder)
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